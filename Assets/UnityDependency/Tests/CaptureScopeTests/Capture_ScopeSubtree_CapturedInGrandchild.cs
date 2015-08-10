namespace UnityDependency.Test.CaptureScope
{
    using Alquimiaware;
    using UnityEngine;
    using UnityDependency.Test.AssertExtensions;
    using UnityDependency.Test.UnityHelpers;

    [IntegrationTest.DynamicTest("CaptureScopeTests")]
    public class Capture_ScopeSubtree_CapturedIngrandchild : MonoBehaviour
    {
        CaptureScopeSample testSubject = null;
        BoxCollider testTarget = null;

        void Start()
        {
            GameObject[] testGOs = UnityHelpers.CreateHierarchy("Subject", "Child", "Grandchild");
            this.testSubject = testGOs[0].AddComponent<CaptureScopeSample>();
            this.testTarget = testGOs[2].AddComponent<BoxCollider>();
        }

        void Update()
        {
            this.testSubject.CaptureDependencies();
            this.testSubject.subtreeCollider.AssertIsSame(this.testTarget);

            DestroyImmediate(this.testSubject.gameObject);
        }
    }
}
