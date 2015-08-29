namespace UnityDependency.Test.CreateDefaultsTests
{
    using Alquimiaware;
    using UnityEngine;
    using UnityHelpers;

    [IntegrationTest.DynamicTest("CreateDefaultsTests")]
    public class Capture_Defaults_CreatedInParent : TestFrame
    {
        private CaptureScopeSampleDefaultPathParent testSubject = null;

        protected override void SetUp()
        {
            GameObject[] gos = UnityHelpers.CreateHierarchy("Parent", "Subject");
            this.testSubject = gos[1].AddComponent<CaptureScopeSampleDefaultPathParent>();
        }

        protected override void Execute()
        {
            this.testSubject.CaptureDependencies();

            this.AssertIsSubtree(this.testSubject.ParentCollider.gameObject, this.testSubject.gameObject, "Subject");
        }

        protected override void TearDown()
        {
            DestroyImmediate(this.testSubject.gameObject.transform.parent.gameObject);
        }
    }
}
