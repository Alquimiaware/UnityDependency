namespace UnityDependency.Test.Capture
{
    using Alquimiaware;
    using UnityEngine;
    using UnityHelpers;

    [IntegrationTest.DynamicTest("CreateDefaultsTests")]
    public class Capture_Defaults_CreatedInChild : TestFrame
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

            this.AssertIsSubtree(this.testSubject.gameObject, this.testSubject.nephewCollider.gameObject, "Child/Nephew");
        }

        protected override void TearDown()
        {
            DestroyImmediate(this.testSubject.transform.parent.parent.gameObject);
        }
    }
}
