namespace UnityDependency.Test
{
using Alquimiaware;
using UnityEngine;

    public class CaptureScopeSample : MonoBehaviour
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

        private void RemoveComponent<T>() where T : Component
        {
            T component = this.GetComponent<T>();
            if (component)
                DestroyImmediate(component);
        }
    }
}
