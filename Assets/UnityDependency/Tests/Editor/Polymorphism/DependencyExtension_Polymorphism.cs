namespace UnityDependency.Test.Polymorphism
{
    using System.Collections.Generic;
    using Alquimiaware;
    using NUnit.Framework;
    using UnityEngine;

    [TestFixture]
    public abstract partial class DependencyExtension_Polymorphism
    {
        protected Builder goBuilder;

        protected class Builder
        {
            private List<GameObject> createdObjects = new List<GameObject>();

            public DependAbstract CreateDependAbstract()
            {
                GameObject created = new GameObject("Dependent");
                var dependent = created.AddComponent<DependAbstract>();
                this.createdObjects.Add(created);
                return dependent;
            }

            public DependDerived CreateDependDerived()
            {
                GameObject created = new GameObject("Dependent");
                var dependent = created.AddComponent<DependDerived>();
                this.createdObjects.Add(created);
                return dependent;
            }

            public DependAbstractDefaultAbstract CreateDependAbstractDefaultAbstract()
            {
                GameObject created = new GameObject("Dependent");
                var dependent = created.AddComponent<DependAbstractDefaultAbstract>();
                this.createdObjects.Add(created);
                return dependent;
            }

            public DependAbstractDefaultDerived CreateDependAbstractDefaultDerived()
            {
                GameObject created = new GameObject("Dependent");
                var dependent = created.AddComponent<DependAbstractDefaultDerived>();
                this.createdObjects.Add(created);
                return dependent;
            }

            public HierarchyDerived CreateDerived()
            {
                GameObject created = new GameObject("Derived");
                var derived = created.AddComponent<HierarchyDerived>();
                this.createdObjects.Add(created);
                return derived;
            }

            public T CreateInRoot<T>(string name)
                where T : Component
            {
                GameObject created = new GameObject(name);
                T comp = created.AddComponent<T>();
                this.createdObjects.Add(created);
                return comp;
            }

            public void TearDown()
            {
                foreach (var obj in this.createdObjects)
                    Object.DestroyImmediate(obj);

                this.createdObjects.Clear();
            }

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

            public class HierarchyDerived : HierarchyAbstract
            {
                [SerializeField]
                [Dependency(Scope.Scene)]
                private SphereCollider derivedField = null;
                public SphereCollider DerivedField { get { return this.derivedField; } }
            }

            public class DependAbstract : MonoBehaviour
            {
                [SerializeField]
                [Dependency(Scope.Scene)]
                private HierarchyAbstract abstractField = null;
                public HierarchyAbstract Abstract { get { return this.abstractField; } }
            }

            public class DependAbstractDefaultAbstract : MonoBehaviour
            {
                [SerializeField]
                [Dependency(Scope.Scene, DefaultType = typeof(HierarchyAbstract))]
                private HierarchyAbstract abstractField = null;
                public HierarchyAbstract Abstract { get { return this.abstractField; } }
            }

            public class DependAbstractDefaultDerived : MonoBehaviour
            {
                [SerializeField]
                [Dependency(Scope.Scene, DefaultType = typeof(HierarchyDerived))]
                private HierarchyDerived derived = null;
                public HierarchyDerived Derived { get { return this.derived; } }
            }

            public class DependDerived : MonoBehaviour
            {
                [SerializeField]
                [Dependency(Scope.Scene)]
                private HierarchyDerived derived = null;
                public HierarchyDerived Derived { get { return this.derived; } }
            }
        }

        [SetUp]
        public void PolymorphismSetUp()
        {
            this.goBuilder = new Builder();
        }

        [TearDown]
        public void PolymorphismTearDown()
        {
            this.goBuilder.TearDown();
            this.goBuilder = null;
        }
    }
}
