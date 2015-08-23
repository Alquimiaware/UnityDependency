namespace UnityDependency.Test.Polymorphism
{
    using Alquimiaware;
    using UnityEngine;

    [IntegrationTest.DynamicTest("PolymorphismTests")]
    public class Polymorphism_DependOnBase_DerivedCaptured : TestFrame
    {
        private BaseContainer testSubject = null;
        private DerivedClass derivedTarget = null;

        protected override void SetUp()
        {
            GameObject container = new GameObject("Container");
            this.testSubject = container.AddComponent<BaseContainer>();

            GameObject derived = new GameObject("Derived");
            this.derivedTarget = derived.AddComponent<DerivedClass>();
        }

        protected override void Execute()
        {
            this.testSubject.CaptureDependencies();
            this.AssertIsSame(this.testSubject.BaseInstance, this.derivedTarget);
        }

        protected override void TearDown()
        {
            DestroyImmediate(this.testSubject.gameObject);
            DestroyImmediate(this.derivedTarget.gameObject);
        }
    }
}
