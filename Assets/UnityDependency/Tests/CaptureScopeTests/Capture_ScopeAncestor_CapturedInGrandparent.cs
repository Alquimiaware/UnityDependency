namespace UnityDependency.Test.CaptureScope
{
    using Alquimiaware;
    using UnityEngine;
    using UnityDependency.Test.AssertExtensions;
    using UnityDependency.Test.UnityHelpers;

    [IntegrationTest.DynamicTest("CaptureScopeTests")]
    public class Capture_ScopeAncestor_CapturedInGrandparent : MonoBehaviour
    {
        CaptureScopeSample testSubject = null;
        SphereCollider testTarget = null;

        void Start()
        {
            GameObject[] testGOs = UnityHelpers.CreateHierarchy("Grandparent", "Parent", "Subject");
            this.testSubject = testGOs[2].AddComponent<CaptureScopeSample>();
            this.testTarget = testGOs[0].AddComponent<SphereCollider>();
        }

        void Update()
        {
            this.testSubject.CaptureDependencies();
            this.testSubject.AncestorCollider.AssertIsSame(this.testTarget);

            DestroyImmediate(this.testTarget.gameObject);
        }
    }
}
