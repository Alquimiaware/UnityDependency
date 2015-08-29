namespace UnityDependency.Test.Polymorphism
{
    using Alquimiaware;
    using UnityEngine;

    public class DerivedClass : AbstractBaseClass
    {
        [SerializeField]
        [Dependency(Scope.Subtree)]
        private SphereCollider derivedField = null;
        public SphereCollider DerivedField { get { return this.derivedField; } }
    }
}
