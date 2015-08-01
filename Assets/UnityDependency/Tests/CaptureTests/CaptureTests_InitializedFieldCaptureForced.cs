using Alquimiaware;
using UnityEngine;
using UnityTest;

namespace UnityDependency.Test.Capture {

    [IntegrationTest.DynamicTest("CaptureTests")]
    public class CaptureTests_InitializedFieldCaptureForced : MonoBehaviour {

        void Start() {
            NephewsColliderCapture testObject = FindObjectOfType<NephewsColliderCapture>();

            var origNephew = testObject.nephewCollider;
            var origRoot = testObject.RootChildCollider;

            testObject.ForceRecaptureDependencies();

            IntegrationTest.Assert(origNephew != testObject.nephewCollider, "NephewCollider has not been recaptured");
            IntegrationTest.Assert(origRoot != testObject.RootChildCollider, "Root child collider has not been recaptured");

            GameObject nephewCollider = testObject.nephewCollider.gameObject;
            IntegrationTest.Assert(nephewCollider.gameObject.name == "Nephew", "NephewCollider not created in a gameobject named 'Nephew', but '" + nephewCollider.gameObject.name + "'");
            IntegrationTest.Assert(nephewCollider.transform.parent != null, "NephewCollider has no parent");
            IntegrationTest.Assert(nephewCollider.transform.parent.gameObject.name == "Child", "NephewCollider not created below a parent gameobject named 'Child', but '" + nephewCollider.gameObject.name + "'");
            IntegrationTest.Assert(nephewCollider.transform.transform.parent != null, "NephewCollider has no grandparent");
            IntegrationTest.Assert(nephewCollider.transform.parent.parent.gameObject == testObject.gameObject, "NephewCollider not created below owner, but '" + nephewCollider.gameObject.name + "'");

            GameObject rootChildCollider = testObject.RootChildCollider.gameObject;
            IntegrationTest.Assert(rootChildCollider.gameObject.name == "Child", "RootChildCollider not created in a gameobject named 'Child', but '" + rootChildCollider.gameObject.name + "'");
            IntegrationTest.Assert(rootChildCollider.transform.parent != null, "RootChildCollider has no parent");
            IntegrationTest.Assert(rootChildCollider.transform.parent.gameObject.name == "Root", "RootChildCollider not created below a parent gameobject named 'Root', but '" + rootChildCollider.gameObject.name + "'");
            IntegrationTest.Assert(rootChildCollider.transform.parent.parent == null, "RootChildCollider should have a root grandparent");
        }
    }
}
