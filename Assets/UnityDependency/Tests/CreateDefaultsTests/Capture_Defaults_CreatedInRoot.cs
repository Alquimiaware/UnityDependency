namespace UnityDependency.Test.Capture
{
    using Alquimiaware;
    using UnityEngine;
    using UnityHelpers;

    [IntegrationTest.DynamicTest("CreateDefaultsTests")]
    public class Capture_Defaults_CreatedInRoot : TestFrame
    {
        private CaptureScopeSampleDefaultPaths testSubject = null;

        protected override void SetUp()
        {
            GameObject[] gos = UnityHelpers.CreateHierarchy("Grandparent", "Parent", "Subject");
            GameObject subjectGO = gos[2];
            this.testSubject = subjectGO.AddComponent<CaptureScopeSampleDefaultPaths>();
        }

        protected override void Execute()
        {
            this.testSubject.CaptureDependencies();

            this.AssertIsTree(this.testSubject.RootChildCollider.gameObject, "Root/Child");
        }

        protected override void TearDown()
        {
            DestroyImmediate(this.testSubject.RootChildCollider.transform.parent.gameObject);
            DestroyImmediate(this.testSubject.transform.parent.parent.gameObject);
        }
    }
}
