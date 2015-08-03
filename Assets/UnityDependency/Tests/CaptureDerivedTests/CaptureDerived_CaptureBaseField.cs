namespace UnityDependency.Test.CaptureDerived
{
    using Alquimiaware;
    using UnityEngine;

    [IntegrationTest.DynamicTest("CaptureDerivedTests")]
    public class CaptureDerived_CaptureBaseField : MonoBehaviour
    {
        void Start()
        {
            DerivedClass derivedClass = FindObjectOfType<DerivedClass>();

            BoxCollider baseCollider = derivedClass.GetOrAddChild("Child1").gameObject.AddComponent<BoxCollider>();
            SphereCollider derivedCollider = derivedClass.GetOrAddChild("Child2").gameObject.AddComponent<SphereCollider>();

            derivedClass.CaptureDependencies();

            IntegrationTest.Assert(derivedClass.BaseField == baseCollider, "Field of base class not captured");
            IntegrationTest.Assert(derivedClass.DerivedField == derivedCollider, "Field of derived class not captured");

            derivedClass.Reset();

            DestroyImmediate(baseCollider.gameObject);
            DestroyImmediate(derivedCollider.gameObject);
        }
    }
}
