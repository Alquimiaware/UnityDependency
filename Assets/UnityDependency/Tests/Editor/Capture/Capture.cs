namespace UnityDependency.Test.Capture
{
    using Alquimiaware;
    using NUnit.Framework;
    using UnityEngine;

    [TestFixture]
    public partial class DependencyExtension_Capture
    {
        [Test]
        public void Capture_DependencyIsPublic_IsCaptured()
        {
            var sut = this.goBuilder.CreateInRoot<PublicDependency>("SUT");
            var candidate = this.goBuilder.CreateInRoot<BoxCollider>("Candidate");
            sut.CaptureDependencies();

            Assert.AreSame(candidate, sut.publicField);
        }

        [Test]
        public void Capture_DependencyIsProtected_IsCaptured()
        {
            var sut = this.goBuilder.CreateInRoot<ProtectedDependency>("SUT");
            var candidate = this.goBuilder.CreateInRoot<BoxCollider>("Candidate");
            sut.CaptureDependencies();

            Assert.AreSame(candidate, sut.ProtectedField);
        }

        [Test]
        public void Capture_DependencyIsPrivate_IsCaptured()
        {
            var sut = this.goBuilder.CreateInRoot<PrivateDependency>("SUT");
            var candidate = this.goBuilder.CreateInRoot<BoxCollider>("Candidate");
            sut.CaptureDependencies();

            Assert.AreSame(candidate, sut.PrivateField);
        }

        [Test]
        public void Capture_DependantIsCaptured_IsNotRecaptured()
        {
            var sut = this.goBuilder.CreateInRoot<PublicDependency>("SUT");
            sut.publicField = this.goBuilder.CreateInRoot<BoxCollider>("Captured");
            var candidate = sut.gameObject.AddComponent<BoxCollider>();
            sut.CaptureDependencies();

            Assert.AreNotSame(candidate, sut.publicField);
        }

        [Test]
        public void Capture_DependantIsMissing_IsCaptured()
        {
            var sut = this.goBuilder.CreateInRoot<PublicDependency>("SUT");
            sut.publicField = this.goBuilder.CreateInRoot<BoxCollider>("Captured");
            GameObject.DestroyImmediate(sut.publicField);
            var candidate = sut.gameObject.AddComponent<BoxCollider>();
            sut.CaptureDependencies();

            Assert.AreSame(candidate, sut.publicField);
        }
    }
}
