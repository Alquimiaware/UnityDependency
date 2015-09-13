namespace UnityDependency.Test.Capture
{
    using System.Collections.Generic;
    using NUnit.Framework;
    using UnityEngine;

    [TestFixture]
    public partial class DependencyExtension_Capture
    {
        protected Builder goBuilder;

        protected class Builder
        {
            private List<GameObject> createdObjects = new List<GameObject>();

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
        public void DependencyExtension_Capture_SetUp()
        {
            this.goBuilder = new Builder();
        }

        [TearDown]
        public void DependencyExtension_Capture_TearDown()
        {
            this.goBuilder.TearDown();
        }
    }
}
