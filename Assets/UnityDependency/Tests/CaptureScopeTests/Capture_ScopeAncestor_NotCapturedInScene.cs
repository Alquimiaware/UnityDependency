namespace UnityDependency.Test.CaptureScope
{
    using Alquimiaware;
    using UnityEngine;

    [IntegrationTest.DynamicTest("CaptureScopeTests")]
    public class Capture_ScopeAncestor_NotCapturedInScene : TestFrame
    {
        CaptureScopeSample testSubject = null;
        SphereCollider testTarget = null;

        protected override void SetUp()
        {
            GameObject subjectGO = new GameObject("Subject");
            GameObject targetGO = new GameObject("Target");
            this.testSubject = subjectGO.AddComponent<CaptureScopeSample>();
            this.testTarget = targetGO.AddComponent<SphereCollider>();
        }

        protected override void Execute()
        {
            this.testSubject.CaptureDependencies();
            this.AssertIsOther(this.testSubject.AncestorCollider, this.testTarget);
        }

        protected override void TearDown()
        {
            DestroyImmediate(this.testSubject.gameObject);
            DestroyImmediate(this.testTarget.gameObject);
        }
    }
}
