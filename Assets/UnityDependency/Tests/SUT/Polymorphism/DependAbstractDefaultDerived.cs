namespace UnityDependency.Test.SUT.Polymorphism
{
    using Alquimiaware;
    using UnityEngine;

    public class DependAbstractDefaultDerived : MonoBehaviour
    {
        [SerializeField]
        [Dependency(Scope.Scene, DefaultType = typeof(HierarchyDerived))]
        private HierarchyDerived derived = null;
        public HierarchyDerived Derived { get { return this.derived; } }
    }
}
