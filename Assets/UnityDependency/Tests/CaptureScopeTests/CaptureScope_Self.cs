using Alquimiaware;
using UnityEngine;

namespace UnityDependency.Test.CaptureScope {

    [IntegrationTest.DynamicTest("CaptureScopeTests")]
    public class CaptureScope_Self : MonoBehaviour {

        // Use this for initialization
        void Start() {
            CaptureScope captureScope = FindObjectOfType<CaptureScope>();
            StartCoroutine(this.CreateAndCapture(captureScope));
        }

        System.Collections.IEnumerator CreateAndCapture(CaptureScope captureScope) {
            BoxCollider subtreeCollider = captureScope.gameObject.AddComponent<BoxCollider>();
            SphereCollider ancestorCollider = captureScope.gameObject.AddComponent<SphereCollider>();
            CapsuleCollider sceneCollider = captureScope.gameObject.AddComponent<CapsuleCollider>();

            yield return null;

            captureScope.CaptureDependencies();

            IntegrationTest.Assert(captureScope.subtreeCollider == subtreeCollider, "Component in self not found with scope '" + Scope.Subtree + "'");
            IntegrationTest.Assert(captureScope.AncestorCollider == ancestorCollider, "Component in self not found with scope '" + Scope.Ancestor + "'");
            IntegrationTest.Assert(captureScope.SceneCollider == sceneCollider, "Component in self not found with scope '" + Scope.Scene + "'");

            DestroyImmediate(sceneCollider);
            DestroyImmediate(ancestorCollider);
            DestroyImmediate(subtreeCollider);

            captureScope.Reset();
        }
    }
}
