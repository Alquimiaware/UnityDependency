namespace UnityDependency.Test.CaptureScope
{
    using Alquimiaware;
    using UnityDependency.Test.UnityHelpers;
    using UnityEngine;

    [IntegrationTest.DynamicTest("CaptureScopeTests")]
    public class Capture_ScopeSubtree_CapturedIngrandchild : TestFrame
    {
        CaptureScopeSample testSubject = null;
        BoxCollider testTarget = null;

        protected override void SetUp()
        {
            GameObject[] testGOs = UnityHelpers.CreateHierarchy("Subject", "Child", "Grandchild");
            this.testSubject = testGOs[0].AddComponent<CaptureScopeSample>();
            this.testTarget = testGOs[2].AddComponent<BoxCollider>();
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
