namespace UnityDependency.Test.CaptureScope
{
    using Alquimiaware;
    using UnityEngine;
    using UnityDependency.Test.AssertExtensions;
    using UnityDependency.Test.UnityHelpers;

    [IntegrationTest.DynamicTest("CaptureScopeTests")]
    public class Capture_ScopeScene_CapturedInScene : MonoBehaviour
    {
        CaptureScopeSample testSubject = null;
        CapsuleCollider testTarget = null;

        void Start()
        {
            GameObject subjectGO = new GameObject("Subject");
            GameObject targetGO = new GameObject("Target");
            this.testSubject = subjectGO.AddComponent<CaptureScopeSample>();
            this.testTarget = targetGO.AddComponent<CapsuleCollider>();
        }

        void Update()
        {
            this.testSubject.CaptureDependencies();
            this.testSubject.SceneCollider.AssertIsSame(this.testTarget);

            DestroyImmediate(this.testSubject.gameObject);
            DestroyImmediate(this.testTarget.gameObject);
        }
    }
}
