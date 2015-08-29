namespace UnityDependency.Test.CaptureScope
{
    using Alquimiaware;
    using UnityEngine;

    [IntegrationTest.DynamicTest("CaptureScopeTests")]
    public class Capture_ScopeSubtree_CapturedInSelf : TestFrame
    {
        CaptureScopeSample testSubject = null;
        BoxCollider testTarget = null;

        protected override void SetUp()
        {
            GameObject testGO = new GameObject("Subject");
            this.testSubject = testGO.AddComponent<CaptureScopeSample>();
            this.testTarget = testGO.AddComponent<BoxCollider>();
        }

        protected override void Execute()
        {
            this.testSubject.CaptureDependencies();
            this.AssertIsSame(this.testSubject.subtreeCollider, this.testTarget);
        }

        protected override void TearDown()
        {
            DestroyImmediate(this.testSubject.gameObject);
        }
    }
}
