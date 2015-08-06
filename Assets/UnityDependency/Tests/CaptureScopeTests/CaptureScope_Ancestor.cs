namespace UnityDependency.Test.CaptureScope
{
    using Alquimiaware;
    using UnityEngine;

    [IntegrationTest.DynamicTest("CaptureScopeTests")]
    public class CaptureScope_Ancestor : MonoBehaviour
    {
        // Use this for initialization
        void Start()
        {
            CaptureScope captureScope = FindObjectOfType<CaptureScope>();
            StartCoroutine(this.CreateAndCapture(captureScope));
        }

        System.Collections.IEnumerator CreateAndCapture(CaptureScope captureScope)
        {
            BoxCollider subtreeCollider = captureScope.transform.parent.gameObject.AddComponent<BoxCollider>();
            SphereCollider ancestorCollider = subtreeCollider.transform.parent.gameObject.AddComponent<SphereCollider>();
            CapsuleCollider sceneCollider = ancestorCollider.transform.parent.gameObject.AddComponent<CapsuleCollider>();

            yield return null;

            captureScope.CaptureDependencies();

            IntegrationTest.Assert(captureScope.subtreeCollider != subtreeCollider, "Component in parent found with scope '" + Scope.Subtree + "'");
            IntegrationTest.Assert(captureScope.AncestorCollider == ancestorCollider, "Component in grandchild not found with scope '" + Scope.Ancestor + "'");
            IntegrationTest.Assert(captureScope.SceneCollider == sceneCollider, "Component in grand-grandchild not found with scope '" + Scope.Scene + "'");

            DestroyImmediate(sceneCollider);
            DestroyImmediate(ancestorCollider);
            DestroyImmediate(subtreeCollider);

            captureScope.Reset();
        }
    }
}
