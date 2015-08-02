using Alquimiaware;
using UnityEngine;

namespace UnityDependency.Test.CaptureDerived {
    public class BaseClass : MonoBehaviour {

        [SerializeField]
        [Dependency(Scope.Subtree)]
        protected BoxCollider baseField = null;
        public BoxCollider BaseField { get { return this.baseField; } }

        public virtual void Reset() {
            this.baseField = null;
            this.RemoveComponent<BoxCollider>();
        }

        protected void RemoveComponent<T>() where T : Component {
            T component = this.GetComponent<T>();
            if (component)
                DestroyImmediate(component);
        }
    }
}
