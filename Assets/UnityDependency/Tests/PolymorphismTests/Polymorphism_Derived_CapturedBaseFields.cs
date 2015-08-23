namespace UnityDependency.Test.Polymorphism
{
    using Alquimiaware;
    using UnityEngine;

    [IntegrationTest.DynamicTest("PolymorphismTests")]
    public class Polymorphism_Derived_CapturedBaseFields : TestFrame
    {
        private DerivedClass testSubject = null;

        protected override void SetUp()
        {
            GameObject derived = new GameObject("Derived");
            this.testSubject = derived.AddComponent<DerivedClass>();
        }

        protected override void Execute()
        {
            this.testSubject.CaptureDependencies();
            this.AssertIsAssigned(this.testSubject.BasePrivateField);
            this.AssertIsAssigned(this.testSubject.BaseProtectedField);
        }

        protected override void TearDown()
        {
            DestroyImmediate(this.testSubject.gameObject);
        }
    }
}
