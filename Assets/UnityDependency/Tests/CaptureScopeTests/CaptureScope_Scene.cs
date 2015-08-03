namespace UnityDependency.Test.CaptureScope
{
    using Alquimiaware;
    using UnityEngine;

    [IntegrationTest.DynamicTest("CaptureScopeTests")]
    public class CaptureScope_Scene : MonoBehaviour
    {
        // Use this for initialization
        void Start()
        {
            CaptureScope captureScope = FindObjectOfType<CaptureScope>();
            StartCoroutine(this.CreateAndCapture(captureScope));
        }

        System.Collections.IEnumerator CreateAndCapture(CaptureScope captureScope)
        {
            BoxCollider subtreeCollider = new GameObject("NewRoot").AddComponent<BoxCollider>();
            SphereCollider ancestorCollider = subtreeCollider.GetOrAddChild("NewRootChild").gameObject.AddComponent<SphereCollider>();
            CapsuleCollider sceneCollider = ancestorCollider.GetOrAddChild("NewRootGrandchild").gameObject.AddComponent<CapsuleCollider>();

            yield return null;

            captureScope.CaptureDependencies();

            IntegrationTest.Assert(captureScope.subtreeCollider != subtreeCollider, "Component in scene root found with scope '" + Scope.Subtree + "'");
            IntegrationTest.Assert(captureScope.AncestorCollider != ancestorCollider, "Component in scene root's child found with scope '" + Scope.Ancestor + "'");
            IntegrationTest.Assert(captureScope.SceneCollider == sceneCollider, "Component in scene root grandchild not found with scope '" + Scope.Scene + "'");

            DestroyImmediate(sceneCollider.gameObject);
            DestroyImmediate(ancestorCollider.gameObject);
            DestroyImmediate(subtreeCollider.gameObject);

            captureScope.Reset();
        }
    }
}
