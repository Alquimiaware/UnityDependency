namespace UnityDependency.Test.Capture
{
    using Alquimiaware;
    using UnityEngine;

    [IntegrationTest.DynamicTest("CaptureTests")]
    public class CaptureTests_InitializedFieldNotCaptured : MonoBehaviour
    {
        void Start()
        {
            CaptureScopeSampleDefaultPaths testObject = FindObjectOfType<CaptureScopeSampleDefaultPaths>();

            var origNephew = testObject.nephewCollider;
            var origRoot = testObject.RootChildCollider;

            testObject.CaptureDependencies();

            IntegrationTest.Assert(origNephew == testObject.nephewCollider, "NephewCollider has been recaptured");
            IntegrationTest.Assert(origRoot == testObject.RootChildCollider, "Root child collider has been recaptured");
        }
    }
}
