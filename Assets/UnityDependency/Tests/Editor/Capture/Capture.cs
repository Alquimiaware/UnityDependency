namespace UnityDependency.Test.Capture
{
    using Alquimiaware;
    using NUnit.Framework;
    using UnityDependency.Test.SUT.Capture;
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

            Assert.AreSame(candidate, sut.Field);
        }

        [Test]
        public void Capture_DependencyIsProtected_IsCaptured()
        {
            var sut = this.goBuilder.CreateInRoot<ProtectedDependency>("SUT");
            var candidate = this.goBuilder.CreateInRoot<BoxCollider>("Candidate");
            sut.CaptureDependencies();

            Assert.AreSame(candidate, sut.Field);
        }

        [Test]
        public void Capture_DependencyIsPrivate_IsCaptured()
        {
            var sut = this.goBuilder.CreateInRoot<PrivateDependency>("SUT");
            var candidate = this.goBuilder.CreateInRoot<BoxCollider>("Candidate");
            sut.CaptureDependencies();

            Assert.AreSame(candidate, sut.Field);
        }

        [Test]
        public void Capture_IsInitialized_IsNotCaptured()
        {
            var sut = this.goBuilder.CreateInRoot<PublicDependency>("SUT");
            sut.field = this.goBuilder.CreateInRoot<BoxCollider>("Captured");
            var candidate = sut.gameObject.AddComponent<BoxCollider>();
            sut.CaptureDependencies();

            Assert.AreNotSame(candidate, sut.Field);
        }

        [Test]
        public void Capture_IsMissing_IsCaptured()
        {
            var sut = this.goBuilder.CreateInRoot<PublicDependency>("SUT");
            sut.field = this.goBuilder.CreateInRoot<BoxCollider>("Captured");
            GameObject.DestroyImmediate(sut.field);
            var candidate = sut.gameObject.AddComponent<BoxCollider>();
            sut.CaptureDependencies();

            Assert.AreSame(candidate, sut.Field);
        }
    }
}
