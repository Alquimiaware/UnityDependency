namespace UnityDependency.Tests
{
    using Alquimiaware;
    using NUnit.Framework;
    using UnityEngine;

    public abstract partial class DependencyExtension_ParentChain
    {
        public class ScopeSubtree : DependencyExtension_ParentChain
        {
            private DependantOnSubtree sut = null;

            [SetUp]
            public void AncestorSetUp()
            {
                this.sut = this.parentChain.Self.AddComponent<DependantOnSubtree>();
            }

            [Test]
            public void Capture_CandidateInSelf_IsCaptured()
            {
                var candidate = this.parentChain.Self.AddComponent<BoxCollider>();
                this.sut.CaptureDependencies();

                Assert.AreSame(sut.dependency, candidate);
            }

            [Test]
            public void Capture_CandidateInChild_IsCaptured()
            {
                var candidate = this.parentChain.Child.AddComponent<BoxCollider>();
                this.sut.CaptureDependencies();

                Assert.AreSame(sut.dependency, candidate);
            }

            [Test]
            public void Capture_CandidateInGrandchild_IsCaptured()
            {
                var candidate = this.parentChain.Grandchild.AddComponent<BoxCollider>();
                this.sut.CaptureDependencies();

                Assert.AreSame(sut.dependency, candidate);
            }

            [Test]
            public void Capture_CandidateInParent_IsNotCaptured()
            {
                var candidate = this.parentChain.Parent.AddComponent<BoxCollider>();
                this.sut.CaptureDependencies();

                Assert.AreNotSame(sut.dependency, candidate);
            }

            [Test]
            public void Capture_CandidateInGrandparent_IsNotCaptured()
            {
                var candidate = this.parentChain.Grandparent.AddComponent<BoxCollider>();
                this.sut.CaptureDependencies();

                Assert.AreNotSame(this.sut.dependency, candidate);
            }

            [Test]
            public void Capture_CandidateInScene_IsNotCaptured()
            {
                var candidate = this.goBuilder.CreateInRoot<BoxCollider>("Candidate");
                this.sut.CaptureDependencies();

                Assert.AreNotSame(sut.dependency, candidate);
            }

            public class DependantOnSubtree : MonoBehaviour
            {
                [Dependency(Scope.Subtree)]
                public BoxCollider dependency = null;
            }
        }
    }
}
