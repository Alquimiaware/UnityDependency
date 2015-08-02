using Alquimiaware;
using UnityEngine;

namespace UnityDependency.Test.CaptureDerived {
    public class DerivedContainer : MonoBehaviour {
        [SerializeField]
        [Dependency(Scope.Scene)]
        private BaseClass baseInstance = null;
        public BaseClass BaseInstance { get { return this.baseInstance; } }

        public void Reset() {
            this.baseInstance = null;
            if (this.GetComponent<BaseClass>() != null)
                DestroyImmediate(this.GetComponent<BaseClass>());
        }
    }
}
