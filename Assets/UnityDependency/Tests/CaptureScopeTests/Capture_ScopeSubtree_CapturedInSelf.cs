namespace UnityDependency.Test.CaptureScope
{
    using Alquimiaware;
    using UnityEngine;
    using UnityDependency.Test.AssertExtensions;

    [IntegrationTest.DynamicTest("CaptureScopeTests")]
    public class Capture_ScopeSubtree_CapturedInSelf : MonoBehaviour
    {
        CaptureScopeSample testSubject = null;
        BoxCollider testTarget = null;

        void Start()
        {
            GameObject testGO = new GameObject("Subject");
            this.testSubject = testGO.AddComponent<CaptureScopeSample>();
            this.testTarget = testGO.AddComponent<BoxCollider>();
        }

        void Update()
        {
            this.testSubject.CaptureDependencies();
            this.testSubject.subtreeCollider.AssertIsSame(this.testTarget);

            DestroyImmediate(this.testSubject.gameObject);
        }
    }
}
