namespace UnityDependency.Test.Capture
{
    using Alquimiaware;
    using UnityEngine;
    using UnityHelpers;

    [IntegrationTest.DynamicTest("CreateDefaultsTests")]
    public class Capture_Defaults_CreatedInSibling : TestFrame
    {
        private CaptureScopeSampleDefaultPaths testSubject = null;
        private GameObject testTarget = null;

        protected override void SetUp()
        {
            GameObject[] gos = UnityHelpers.CreateHierarchy("Grandparent", "Parent", "Subject");
            this.testSubject = gos[2].AddComponent<CaptureScopeSampleDefaultPaths>();

            this.testTarget = new GameObject("Sibling");
            this.testTarget.transform.parent = gos[1].transform;
        }

        protected override void Execute()
        {
            this.testSubject.CaptureDependencies();

            this.AssertIsSame(this.testSubject.SiblingCollider.gameObject, this.testTarget);
        }

        protected override void TearDown()
        {
            DestroyImmediate(this.testSubject.transform.parent.parent.gameObject);
            DestroyImmediate(this.testTarget.gameObject);
        }
    }
}
