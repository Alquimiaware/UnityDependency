namespace UnityDependency.Test.Capture
{
    using Alquimiaware;
    using UnityEngine;

    [IntegrationTest.DynamicTest("CreateDefaultsTests")]
    public class CreateDefaults_SubtreeRoot : MonoBehaviour
    {

        private NephewsColliderCapture testedBehaviour = null;

        void Start()
        {
            this.testedBehaviour = FindObjectOfType<NephewsColliderCapture>();

            this.testedBehaviour.CaptureDependencies();

            IntegrationTest.Assert(this.testedBehaviour.nephewCollider != null, "NephewCollider has not been captured");
            IntegrationTest.Assert(this.testedBehaviour.RootChildCollider != null, "RootChildCollider has not been captured");

            GameObject nephewCollider = this.testedBehaviour.nephewCollider.gameObject;
            IntegrationTest.Assert(nephewCollider.gameObject.name == "Nephew", "NephewCollider not created in a gameobject named 'Nephew', but '" + nephewCollider.gameObject.name + "'");
            IntegrationTest.Assert(nephewCollider.transform.parent != null, "NephewCollider has no parent");
            IntegrationTest.Assert(nephewCollider.transform.parent.gameObject.name == "Child", "NephewCollider not created below a parent gameobject named 'Child', but '" + nephewCollider.gameObject.name + "'");
            IntegrationTest.Assert(nephewCollider.transform.transform.parent != null, "NephewCollider has no grandparent");
            IntegrationTest.Assert(nephewCollider.transform.parent.parent.gameObject == this.testedBehaviour.gameObject, "NephewCollider not created below owner, but '" + nephewCollider.gameObject.name + "'");

            GameObject rootChildCollider = this.testedBehaviour.RootChildCollider.gameObject;
            IntegrationTest.Assert(rootChildCollider.gameObject.name == "Child", "RootChildCollider not created in a gameobject named 'Child', but '" + rootChildCollider.gameObject.name + "'");
            IntegrationTest.Assert(rootChildCollider.transform.parent != null, "RootChildCollider has no parent");
            IntegrationTest.Assert(rootChildCollider.transform.parent.gameObject.name == "Root", "RootChildCollider not created below a parent gameobject named 'Root', but '" + rootChildCollider.gameObject.name + "'");
            IntegrationTest.Assert(rootChildCollider.transform.parent.parent == null, "RootChildCollider should have a root grandparent");
        }

    }
}
