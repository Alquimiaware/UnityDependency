namespace UnityDependency.Test.Capture
{
    using Alquimiaware;
    using UnityEngine;
    using AssertExtensions;
    using UnityHelpers;

    [IntegrationTest.DynamicTest("CreateDefaultsTests")]
    public class Capture_Defaults_CreatedInSibling : MonoBehaviour
    {
        private CaptureScopeSampleDefaultPaths testSubject = null;
        private GameObject testTarget = null;

        void Start()
        {
            GameObject[] gos = UnityHelpers.CreateHierarchy("Grandparent", "Parent", "Subject");
            this.testSubject = gos[2].AddComponent<CaptureScopeSampleDefaultPaths>();

            this.testTarget = new GameObject("Sibling");
            this.testTarget.transform.parent = gos[1].transform;
        }

        void Update()
        {
            try
            {
                this.testSubject.CaptureDependencies();

                this.testSubject.SiblingCollider.gameObject.AssertIsSame(this.testTarget);
            }
            catch
            {
                throw;
            }
            finally
            {
                DestroyImmediate(this.testSubject.transform.parent.parent.gameObject);
                DestroyImmediate(this.testTarget.gameObject);
            }
        }
    }
}
