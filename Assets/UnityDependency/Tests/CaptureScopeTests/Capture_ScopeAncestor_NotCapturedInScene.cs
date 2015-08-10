namespace UnityDependency.Test.CaptureScope
{
    using Alquimiaware;
    using UnityEngine;
    using UnityDependency.Test.AssertExtensions;
    using UnityDependency.Test.UnityHelpers;

    [IntegrationTest.DynamicTest("CaptureScopeTests")]
    public class Capture_ScopeAncestor_NotCapturedInScene : MonoBehaviour
    {
        CaptureScopeSample testSubject = null;
        SphereCollider testTarget = null;

        void Start()
        {
            GameObject subjectGO = new GameObject("Subject");
            GameObject targetGO = new GameObject("Target");
            this.testSubject = subjectGO.AddComponent<CaptureScopeSample>();
            this.testTarget = targetGO.AddComponent<SphereCollider>();
        }

        void Update()
        {
            this.testSubject.CaptureDependencies();
            this.testSubject.AncestorCollider.AssertIsOther(this.testTarget);

            DestroyImmediate(this.testSubject.gameObject);
            DestroyImmediate(this.testTarget.gameObject);
        }
    }
}
