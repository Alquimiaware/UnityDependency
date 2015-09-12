namespace UnityDependency.Test.CreateDefaultsTests
{
    using Alquimiaware;
    using UnityEngine;
    using UnityHelpers;

    [IntegrationTest.DynamicTest("CreateDefaultsTests")]
    public class Capture_Defaults_CreatedInRootWithSlash : TestFrame
    {
        private CaptureSampleDefaultPathWithSlash testSubject = null;

        protected override void SetUp()
        {
            this.testSubject = new GameObject("Subject").AddComponent<CaptureSampleDefaultPathWithSlash>();
        }

        protected override void Execute()
        {
            this.testSubject.CaptureDependencies();

            this.AssertIsTree(this.testSubject.RootCollider.gameObject, "Root");
        }

        protected override void TearDown()
        {
            DestroyImmediate(this.testSubject.RootCollider.gameObject);
            DestroyImmediate(this.testSubject.gameObject);
        }
    }
}
