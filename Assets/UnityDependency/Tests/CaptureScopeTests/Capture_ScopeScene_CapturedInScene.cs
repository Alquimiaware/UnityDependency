namespace UnityDependency.Test.CaptureScope
{
    using Alquimiaware;
    using UnityEngine;

    [IntegrationTest.DynamicTest("CaptureScopeTests")]
    public class Capture_ScopeScene_CapturedInScene : TestFrame
    {
        CaptureScopeSample testSubject = null;
        CapsuleCollider testTarget = null;

        protected override void SetUp()
        {
            GameObject subjectGO = new GameObject("Subject");
            GameObject targetGO = new GameObject("Target");
            this.testSubject = subjectGO.AddComponent<CaptureScopeSample>();
            this.testTarget = targetGO.AddComponent<CapsuleCollider>();
        }

        protected override void Execute()
        {
            this.testSubject.CaptureDependencies();
            this.AssertIsSame(this.testSubject.SceneCollider, this.testTarget);
        }

        protected override void TearDown()
        {
            DestroyImmediate(this.testSubject.gameObject);
            DestroyImmediate(this.testTarget.gameObject);
        }
    }
}
