namespace UnityDependency.Test.Capture
{
    using Alquimiaware;
    using UnityEngine;

    [IntegrationTest.DynamicTest("CaptureMissingFieldsTests")]
    public class CaptureMissingFields_PrivatePublic : MonoBehaviour
    {
        private NephewsColliderCapture testObject = null;

        void Start()
        {
            this.testObject = FindObjectOfType<NephewsColliderCapture>();

            var nephew = new GameObject("Temp_Nephew");
            var nephewCollider = nephew.AddComponent<BoxCollider>();
            this.testObject.nephewCollider = nephewCollider;
            DestroyImmediate(nephew);

            var rootChild = new GameObject("Temp_RootChild");
            var rootChildCollider = rootChild.AddComponent<SphereCollider>();
            this.testObject.RootChildCollider = rootChildCollider;
            DestroyImmediate(rootChild);

            IntegrationTest.Assert(this.testObject.nephewCollider == null, "Nephew collider not removed");
            IntegrationTest.Assert(this.testObject.RootChildCollider == null, "RootChild collider not removed");

            this.testObject.CaptureDependencies();

            IntegrationTest.Assert(this.testObject.nephewCollider != null, "Nephew collider not captured");
            IntegrationTest.Assert(this.testObject.RootChildCollider != null, "RootChild collider not captured");
        }

    }
}
