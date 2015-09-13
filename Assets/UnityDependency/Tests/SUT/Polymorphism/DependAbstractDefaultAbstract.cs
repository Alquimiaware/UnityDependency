namespace UnityDependency.Test.SUT.Polymorphism
{
    using Alquimiaware;
    using UnityEngine;

    public class DependAbstractDefaultAbstract : MonoBehaviour
    {
        [SerializeField]
        [Dependency(Scope.Scene, DefaultType = typeof(HierarchyAbstract))]
        private HierarchyAbstract abstractField = null;
        public HierarchyAbstract Abstract { get { return this.abstractField; } }
    }
}
