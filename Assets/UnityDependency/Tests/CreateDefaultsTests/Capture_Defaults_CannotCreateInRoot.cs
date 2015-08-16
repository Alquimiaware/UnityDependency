namespace UnityDependency.Test.CreateDefaultsTests
{
    using Alquimiaware;
    using UnityEngine;

    /// <summary>
    /// This test verifies that, on a component at the root of the scene,
    /// creating a default value at "../." fails,
    /// instead of reverting that "." to the starting element.
    /// </summary>
    [IntegrationTest.DynamicTest("CreateDefaultsTests")]
    [IntegrationTest.ExpectExceptions(succeedOnException = true, exceptionTypeNames = new string[] { "ArgumentOutOfRangeException" })]
    public class Capture_Defaults_CannotCreateInRoot : TestFrame
    {
        private CaptureScopeSampleDefaultPathParentSelf testSubject = null;

        protected override void SetUp()
        {
            GameObject subjectGO = new GameObject("Subject");
            this.testSubject = subjectGO.AddComponent<CaptureScopeSampleDefaultPathParentSelf>();
        }

        protected override void Execute()
        {
            this.testSubject.CaptureDependencies();
        }

        protected override void TearDown()
        {
            DestroyImmediate(this.testSubject.gameObject);
        }
    }
}
