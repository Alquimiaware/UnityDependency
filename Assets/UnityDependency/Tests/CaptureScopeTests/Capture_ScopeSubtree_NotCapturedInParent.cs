namespace UnityDependency.Test.CaptureScope
{
    using Alquimiaware;
    using UnityEngine;
    using UnityDependency.Test.AssertExtensions;
    using UnityDependency.Test.UnityHelpers;

    [IntegrationTest.DynamicTest("CaptureScopeTests")]
    public class Capture_ScopeSubtree_NotCapturedInParent : MonoBehaviour
    {
        CaptureScopeSample testSubject = null;
        BoxCollider testTarget = null;

        void Start()
        {
            GameObject[] testGOs = UnityHelpers.CreateHierarchy("Parent", "Subject");
            this.testSubject = testGOs[1].AddComponent<CaptureScopeSample>();
            this.testTarget = testGOs[0].AddComponent<BoxCollider>();
        }

        void Update()
        {
            this.testSubject.CaptureDependencies();
            this.testSubject.subtreeCollider.AssertIsOther(this.testTarget);

            DestroyImmediate(this.testTarget.gameObject);
        }
    }
}
