namespace UnityDependency.Test.CaptureDerived
{
    using Alquimiaware;
    using UnityEngine;

    [IntegrationTest.DynamicTest("CaptureDerivedTests")]
    public class CaptureDerived_CaptureBaseClass : MonoBehaviour
    {
        void Start()
        {
            DerivedContainer derivedContainer = FindObjectOfType<DerivedContainer>();
            DerivedClass derivedInstance = FindObjectOfType<DerivedClass>();

            derivedContainer.CaptureDependencies();

            IntegrationTest.Assert(derivedContainer.BaseInstance == derivedInstance);

            derivedContainer.Reset();
        }
    }
}
