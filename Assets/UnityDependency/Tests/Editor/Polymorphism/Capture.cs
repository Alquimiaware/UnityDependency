namespace UnityDependency.Test.Polymorphism
{
    using Alquimiaware;
    using NUnit.Framework;
    using UnityEngine;

    public abstract partial class DependencyExtension_Polymorphism
    {
        public class Capture : DependencyExtension_Polymorphism
        {
            [Test]
            public void Capture_PrivateDependencyInBase_IsCaptured()
            {
                var sut = this.goBuilder.CreateDerived();
                var candidate = this.goBuilder.CreateInRoot<CapsuleCollider>("Candidate");
                sut.CaptureDependencies();
                Assert.AreSame(sut.BasePrivateField, candidate);
            }

            [Test]
            public void Capture_ProtectedDependencyInBase_IsCaptured()
            {
                var sut = this.goBuilder.CreateDerived();
                var candidate = this.goBuilder.CreateInRoot<BoxCollider>("Candidate");
                sut.CaptureDependencies();
                Assert.AreSame(sut.BaseProtectedField, candidate);
            }

            [Test]
            public void Capture_PrivateDependencyInDerived_IsCaptured()
            {
                var sut = this.goBuilder.CreateDerived();
                var candidate = this.goBuilder.CreateInRoot<SphereCollider>("Candidate");
                sut.CaptureDependencies();
                Assert.AreSame(sut.DerivedField, candidate);
            }
        }
    }
}
