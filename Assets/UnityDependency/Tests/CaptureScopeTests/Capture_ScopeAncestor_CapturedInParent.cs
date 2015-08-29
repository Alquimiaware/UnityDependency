namespace UnityDependency.Test.CaptureScope
{
    using Alquimiaware;
    using UnityDependency.Test.UnityHelpers;
    using UnityEngine;

    [IntegrationTest.DynamicTest("CaptureScopeTests")]
    public class Capture_ScopeAncestor_CapturedInParent : TestFrame
    {
        CaptureScopeSample testSubject = null;
        SphereCollider testTarget = null;

        protected override void SetUp()
        {
            GameObject[] testGOs = UnityHelpers.CreateHierarchy("Parent", "Subject");
            this.testSubject = testGOs[1].AddComponent<CaptureScopeSample>();
            this.testTarget = testGOs[0].AddComponent<SphereCollider>();
        }

        protected override void Execute()
        {
            this.testSubject.CaptureDependencies();
            this.AssertIsSame(this.testSubject.AncestorCollider, this.testTarget);
        }

        protected override void TearDown()
        {
            DestroyImmediate(this.testTarget.gameObject);
        }
    }
}
