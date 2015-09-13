namespace UnityDependency.Test.Polymorphism
{
    using System.Collections.Generic;
    using UnityDependency.Test.SUT.Polymorphism;
    using NUnit.Framework;
    using UnityEngine;

    [TestFixture]
    public abstract partial class DependencyExtension_Polymorphism
    {
        protected Builder goBuilder;

        protected class ParentChain
        {
            public GameObject Grandparent { get; set; }
            public GameObject Parent { get; set; }
            public GameObject Self { get; set; }
            public GameObject Child { get; set; }
            public GameObject Grandchild { get; set; }
        }

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
