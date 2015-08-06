namespace UnityDependency.Test.CaptureScope
{
    using Alquimiaware;
    using UnityEngine;

    [IntegrationTest.DynamicTest("CaptureScopeTests")]
    public class CaptureScope_Subtree : MonoBehaviour
    {
        // Use this for initialization
        void Start()
        {
            CaptureScope captureScope = FindObjectOfType<CaptureScope>();
            StartCoroutine(this.CreateAndCapture(captureScope));
        }

        System.Collections.IEnumerator CreateAndCapture(CaptureScope captureScope)
        {
            BoxCollider subtreeCollider = captureScope.GetOrAddChild("Child").gameObject.AddComponent<BoxCollider>();
            SphereCollider ancestorCollider = subtreeCollider.GetOrAddChild("Grandchild").gameObject.AddComponent<SphereCollider>();
            CapsuleCollider sceneCollider = ancestorCollider.GetOrAddChild("Grand-grandchild").gameObject.AddComponent<CapsuleCollider>();

            yield return null;

            captureScope.CaptureDependencies();

            IntegrationTest.Assert(captureScope.subtreeCollider == subtreeCollider, "Component in child not found with scope '" + Scope.Subtree + "'");
            IntegrationTest.Assert(captureScope.AncestorCollider == ancestorCollider, "Component in grandchild not found with scope '" + Scope.Ancestor + "'");
            IntegrationTest.Assert(captureScope.SceneCollider == sceneCollider, "Component in grand-grandchild not found with scope '" + Scope.Scene + "'");

            DestroyImmediate(sceneCollider.gameObject);
            DestroyImmediate(ancestorCollider.gameObject);
            DestroyImmediate(subtreeCollider.gameObject);

            captureScope.Reset();
        }
    }
}
