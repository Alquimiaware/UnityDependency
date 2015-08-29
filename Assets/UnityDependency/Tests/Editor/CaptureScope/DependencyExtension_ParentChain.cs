namespace UnityDependency.Test.CaptureScope
{
    using System.Collections.Generic;
    using NUnit.Framework;
    using UnityEngine;

    [TestFixture]
    public abstract partial class DependencyExtension_ParentChain
    {
        protected Builder goBuilder;
        protected ParentChain parentChain;
        protected CaptureScopeSample sut;

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

            public ParentChain CreateHierarchy()
            {
                string[] names = new string[] { "Grandparent", "Parent", "Self", "Child", "Grandchild" };

                GameObject[] created = new GameObject[names.Length];
                for (int i = 0; i < names.Length; ++i)
                {
                    created[i] = new GameObject(names[i]);
                    if (i > 0)
                        created[i].transform.parent = created[i - 1].transform;
                }

                this.createdObjects.Add(created[0]);

                return new ParentChain() {
                    Grandparent = created[0],
                    Parent = created[1],
                    Self = created[2],
                    Child = created[3],
                    Grandchild = created[4]
                };
            }

            public GameObject CreateInRoot(string name = null)
            {
                GameObject newObj = string.IsNullOrEmpty(name)
                    ? new GameObject()
                    : new GameObject(name);

                this.createdObjects.Add(newObj);

                return newObj;
            }

            public T CreateInRoot<T>(string name = null) where T : Component
            {
                return this.CreateInRoot(name).AddComponent<T>();
            }

            public void TearDown()
            {
                foreach (var obj in this.createdObjects)
                    Object.DestroyImmediate(obj);

                this.createdObjects.Clear();
            }
        }

        [SetUp]
        public void ParentChainSetUp()
        {
            this.goBuilder = new Builder();
            this.parentChain = this.goBuilder.CreateHierarchy();
            this.parentChain.Grandparent.name = this.parentChain.Grandparent.name;
            this.sut = this.parentChain.Self.AddComponent<CaptureScopeSample>();
        }

        [TearDown]
        public void ParentChainTearDown()
        {
            this.goBuilder.TearDown();
            this.goBuilder = null;
            this.parentChain = null;
            this.sut = null;
        }
    }
}
