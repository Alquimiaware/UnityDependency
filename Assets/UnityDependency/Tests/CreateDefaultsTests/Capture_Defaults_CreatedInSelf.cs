namespace UnityDependency.Test.Capture
{
    using Alquimiaware;
    using UnityEngine;
    using AssertExtensions;

    [IntegrationTest.DynamicTest("CreateDefaultsTests")]
    public class Capture_Defaults_CreatedInSelf : MonoBehaviour
    {
        private CaptureScopeSample testSubject = null;

        void Start()
        {
            GameObject subjectGO = new GameObject("Subject");
            this.testSubject = subjectGO.AddComponent<CaptureScopeSample>();
        }

        void Update()
        {
            try
            {
                this.testSubject.CaptureDependencies();

                this.testSubject.gameObject.AssertContainsComponents(this.testSubject.subtreeCollider,
                                                                     this.testSubject.AncestorCollider,
                                                                     this.testSubject.SceneCollider);
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
