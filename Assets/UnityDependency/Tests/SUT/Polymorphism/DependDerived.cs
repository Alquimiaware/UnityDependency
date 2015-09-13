namespace UnityDependency.Test.SUT.Polymorphism
{
    using Alquimiaware;
    using UnityEngine;

    public class DependDerived : MonoBehaviour
    {
        [SerializeField]
        [Dependency(Scope.Scene)]
        private HierarchyDerived derived = null;
        public HierarchyDerived Derived { get { return this.derived; } }
    }
}
