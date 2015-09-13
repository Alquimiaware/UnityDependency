namespace UnityDependency.Test.SUT.Polymorphism
{
    using Alquimiaware;
    using UnityEngine;

    public abstract class HierarchyAbstract : MonoBehaviour
    {
        [SerializeField]
        [Dependency(Scope.Scene)]
        protected BoxCollider baseProtectedField = null;
        public BoxCollider BaseProtectedField { get { return this.baseProtectedField; } }

        [SerializeField]
        [Dependency(Scope.Scene)]
        private CapsuleCollider basePrivateField = null;
        public CapsuleCollider BasePrivateField { get { return this.basePrivateField; } }
    }
}
