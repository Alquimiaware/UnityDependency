namespace UnityDependency.Test.Polymorphism
{
    using Alquimiaware;
    using UnityEngine;

    public abstract class AbstractBaseClass : MonoBehaviour
    {
        [SerializeField]
        [Dependency(Scope.Subtree)]
        protected BoxCollider baseProtectedField = null;
        public BoxCollider BaseProtectedField { get { return this.baseProtectedField; } }

        [SerializeField]
        [Dependency(Scope.Scene)]
        private CapsuleCollider basePrivateField = null;
        public CapsuleCollider BasePrivateField { get { return this.basePrivateField; } }
    }
}
