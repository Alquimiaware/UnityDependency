namespace UnityDependency.Test.CaptureScope
{
    using Alquimiaware;
    using UnityDependency.Test.UnityHelpers;
    using UnityEngine;

    [IntegrationTest.DynamicTest("CaptureScopeTests")]
    public class Capture_ScopeSubtree_CapturedInChild : TestFrame
    {
        CaptureScopeSample testSubject = null;
        BoxCollider testTarget = null;

        protected override void SetUp()
        {
            GameObject[] testGOs = UnityHelpers.CreateHierarchy("Subject", "Child");
            this.testSubject = testGOs[0].AddComponent<CaptureScopeSample>();
            this.testTarget = testGOs[1].AddComponent<BoxCollider>();
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
