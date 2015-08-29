namespace UnityDependency.Test.CaptureScope
{
    using Alquimiaware;
    using UnityDependency.Test.UnityHelpers;
    using UnityEngine;

    [IntegrationTest.DynamicTest("CaptureScopeTests")]
    public class Capture_ScopeSubtree_NotCapturedInParent : TestFrame
    {
        CaptureScopeSample testSubject = null;
        BoxCollider testTarget = null;

        protected override void SetUp()
        {
            GameObject[] testGOs = UnityHelpers.CreateHierarchy("Parent", "Subject");
            this.testSubject = testGOs[1].AddComponent<CaptureScopeSample>();
            this.testTarget = testGOs[0].AddComponent<BoxCollider>();
        }

        protected override void Execute()
        {
            this.testSubject.CaptureDependencies();
            this.AssertIsOther(this.testSubject.subtreeCollider, this.testTarget);
        }

        protected override void TearDown()
        {
            DestroyImmediate(this.testTarget.gameObject);
        }
    }
}
