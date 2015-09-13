namespace UnityDependency.Test.SUT.Polymorphism
{
    using Alquimiaware;
    using UnityEngine;

    public class HierarchyDerived : HierarchyAbstract
    {
        [SerializeField]
        [Dependency(Scope.Scene)]
        private SphereCollider derivedField = null;
        public SphereCollider DerivedField { get { return this.derivedField; } }
    }
}
