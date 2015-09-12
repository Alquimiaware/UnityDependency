namespace UnityDependency.Test.Polymorphism
{
    using Alquimiaware;
    using UnityEngine;

    [IntegrationTest.DynamicTest("PolymorphismTests")]
    public class Polymorphism_DependOnAbstract_DefaultFails : TestFrame
    {
        private BaseContainer testSubject = null;

        protected override void SetUp()
        {
            GameObject container = new GameObject("Container");
            this.testSubject = container.AddComponent<BaseContainer>();
        }

        protected override void Execute()
        {
            this.testSubject.CaptureDependencies();
            this.AssertIsUnassigned(this.testSubject.BaseInstance);
        }

        protected override void TearDown()
        {
            DestroyImmediate(this.testSubject.gameObject);
        }
    }
}
