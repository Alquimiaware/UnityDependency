namespace UnityDependency.Test.CaptureScope
{
using Alquimiaware;
using UnityEngine;

    public class CaptureScope : MonoBehaviour
    {
        [SerializeField]
        [Dependency(Scope.Subtree)]
        public BoxCollider subtreeCollider = null;

        [SerializeField]
        [Dependency(Scope.Ancestor)]
        private SphereCollider ancestorCollider = null;
        public SphereCollider AncestorCollider
        {
            get { return this.ancestorCollider; }
            set { this.ancestorCollider = value; }
        }

        [SerializeField]
        [Dependency(Scope.Scene)]
        protected CapsuleCollider sceneCollider = null;
        public CapsuleCollider SceneCollider
        {
            get { return this.sceneCollider; }
            set { this.sceneCollider = value; }
        }

        public void Reset()
        {
            this.subtreeCollider = null;
            this.ancestorCollider = null;
            this.sceneCollider = null;

            this.RemoveComponent<BoxCollider>();
            this.RemoveComponent<SphereCollider>();
            this.RemoveComponent<CapsuleCollider>();
        }

        private void RemoveComponent<T>() where T : Component
        {
            T component = this.GetComponent<T>();
            if (component)
                DestroyImmediate(component);
        }
    }
}
