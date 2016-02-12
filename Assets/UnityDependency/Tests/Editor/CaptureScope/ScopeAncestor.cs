namespace UnityDependency.Tests
{
    using Alquimiaware;
    using NUnit.Framework;
    using UnityEngine;

    public abstract partial class DependencyExtension_ParentChain
    {
        public class ScopeAncestor : DependencyExtension_ParentChain
        {
            private DependantOnAncestor sut = null;

            [SetUp]
            public void AncestorSetUp()
            {
                this.sut = this.parentChain.Self.AddComponent<DependantOnAncestor>();
            }

            [Test]
            public void Capture_CandidateInSelf_IsCaptured()
            {
                var candidate = this.parentChain.Self.AddComponent<SphereCollider>();
                this.sut.CaptureDependencies();

                Assert.AreSame(sut.dependency, candidate);
            }

            [Test]
            public void Capture_CandidateInChild_IsCaptured()
            {
                var candidate = this.parentChain.Child.AddComponent<SphereCollider>();
                this.sut.CaptureDependencies();

                Assert.AreSame(sut.dependency, candidate);
            }

            [Test]
            public void Capture_CandidateInGrandchild_IsCaptured()
            {
                var candidate = this.parentChain.Grandchild.AddComponent<SphereCollider>();
                this.sut.CaptureDependencies();

                Assert.AreSame(sut.dependency, candidate);
            }

            [Test]
            public void Capture_CandidateInParent_IsCaptured()
            {
                var candidate = this.parentChain.Parent.AddComponent<SphereCollider>();
                this.sut.CaptureDependencies();

                Assert.AreSame(this.sut.dependency, candidate);
            }

            [Test]
            public void Capture_CandidateInGrandparent_IsCaptured()
            {
                var candidate = this.parentChain.Grandparent.AddComponent<SphereCollider>();
                this.sut.CaptureDependencies();

                Assert.AreSame(this.sut.dependency, candidate);
            }

            [Test]
            public void Capture_CandidateInScene_IsNotCaptured()
            {
                var candidate = this.goBuilder.CreateInRoot<SphereCollider>("Candidate");
                this.sut.CaptureDependencies();

                Assert.AreNotSame(this.sut.dependency, candidate);
            }

            public class DependantOnAncestor : MonoBehaviour
            {
                [Dependency(Scope.Ancestor)]
                public SphereCollider dependency = null;
            }
        }
    }
}
