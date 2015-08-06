﻿namespace UnityDependency.Test.CaptureDerived
{
    using Alquimiaware;
    using UnityEngine;

    public class DerivedClass : BaseClass
    {
        [SerializeField]
        [Dependency(Scope.Subtree)]
        private SphereCollider derivedField = null;
        public SphereCollider DerivedField { get { return this.derivedField; } }

        public override void Reset()
        {
            base.Reset();
            this.derivedField = null;
            this.RemoveComponent<SphereCollider>();
        }
    }
}