namespace UnityDependency.Test.CreateDefaultsTests
{
    using Alquimiaware;
    using AssertExtensions;
    using UnityEngine;
    using UnityHelpers;

    /// <summary>
    /// This test verifies that, on a component at the root of the scene,
    /// creating a default value at "../." fails,
    /// instead of reverting that "." to the starting element.
    /// </summary>
    [IntegrationTest.DynamicTest("CreateDefaultsTests")]
    [IntegrationTest.ExpectExceptions(succeedOnException = true, exceptionTypeNames = new string[] { "ArgumentOutOfRangeException" })]
    public class Capture_Defaults_CannotCreateInRoot : MonoBehaviour
    {
        private CaptureScopeSampleDefaultPathParentSelf testSubject = null;

        void Start()
        {
            GameObject subjectGO = new GameObject("Subject");
            this.testSubject = subjectGO.AddComponent<CaptureScopeSampleDefaultPathParentSelf>();
        }

        void Update()
        {
            try
            {
                this.testSubject.CaptureDependencies();
            }
            catch
            {
                throw;
            }
            finally
            {
                DestroyImmediate(this.testSubject.gameObject);
            }
        }
    }
}
