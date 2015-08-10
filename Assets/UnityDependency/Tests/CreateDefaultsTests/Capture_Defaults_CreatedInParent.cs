namespace UnityDependency.Test.CreateDefaultsTests
{
    using Alquimiaware;
    using AssertExtensions;
    using UnityEngine;
    using UnityHelpers;

    [IntegrationTest.DynamicTest("CreateDefaultsTests")]
    public class Capture_Defaults_CreatedInParent : MonoBehaviour
    {
        private CaptureScopeSampleDefaultPathParent testSubject = null;

        void Start()
        {
            GameObject[] gos = UnityHelpers.CreateHierarchy("Parent", "Subject");
            this.testSubject = gos[1].AddComponent<CaptureScopeSampleDefaultPathParent>();
        }

        void Update()
        {
            try
            {
                this.testSubject.CaptureDependencies();
                this.testSubject.ParentCollider.gameObject.AssertIsSubtree(this.testSubject.gameObject, "Subject");
            }
            catch
            {
                throw;
            }
            finally
            {
                DestroyImmediate(this.testSubject.gameObject.transform.parent.gameObject);
            }
        }
    }
}
