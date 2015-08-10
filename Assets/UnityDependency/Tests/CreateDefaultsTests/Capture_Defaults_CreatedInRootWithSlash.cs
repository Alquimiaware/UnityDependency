namespace UnityDependency.Test.CreateDefaultsTests
{
    using UnityEngine;
    using UnityHelpers;
    using Alquimiaware;
    using AssertExtensions;

    [IntegrationTest.DynamicTest("CreateDefaultsTests")]
    public class Capture_Defaults_CreatedInRootWithSlash : MonoBehaviour
    {
        private CaptureSampleDefaultPathWithSlash testSubject = null;

        void Start()
        {
            this.testSubject = new GameObject("Subject").AddComponent<CaptureSampleDefaultPathWithSlash>();
        }

        void Update()
        {
            try
            {
                this.testSubject.CaptureDependencies();

                this.testSubject.RootCollider.gameObject.AssertIsTree("Root");
            }
            catch
            {
                throw;
            }
            finally
            {
                DestroyImmediate(this.testSubject.RootCollider.gameObject);
                DestroyImmediate(this.testSubject.gameObject);
            }
        }
    }
}
