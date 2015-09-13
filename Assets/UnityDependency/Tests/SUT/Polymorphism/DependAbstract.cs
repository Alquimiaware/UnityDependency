namespace UnityDependency.Test.SUT.Polymorphism
{
    using Alquimiaware;
    using UnityEngine;

    public class DependAbstract : MonoBehaviour
    {
        [SerializeField]
        [Dependency(Scope.Scene)]
        private HierarchyAbstract abstractField = null;
        public HierarchyAbstract Abstract { get { return this.abstractField; } }
    }
}
