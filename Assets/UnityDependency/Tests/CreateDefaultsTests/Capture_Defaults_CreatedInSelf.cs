namespace UnityDependency.Test.Capture
{
    using Alquimiaware;
    using UnityEngine;

    [IntegrationTest.DynamicTest("CreateDefaultsTests")]
    public class Capture_Defaults_CreatedInSelf : TestFrame
    {
        private CaptureScopeSample testSubject = null;

        protected override void SetUp()
        {
            GameObject subjectGO = new GameObject("Subject");
            this.testSubject = subjectGO.AddComponent<CaptureScopeSample>();
        }

        protected override void Execute()
        {
            this.testSubject.CaptureDependencies();

            this.AssertContainsComponents(this.testSubject.gameObject,
                                          this.testSubject.subtreeCollider,
                                          this.testSubject.AncestorCollider,
                                          this.testSubject.SceneCollider);
        }

        protected override void TearDown()
        {
            DestroyImmediate(this.testSubject.gameObject);
        }
    }
}
