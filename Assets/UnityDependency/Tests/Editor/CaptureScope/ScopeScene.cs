namespace UnityDependency.Tests
{
    using Alquimiaware;
    using NUnit.Framework;
    using UnityEngine;

    public abstract partial class DependencyExtension_ParentChain
    {
        public class ScopeScene : DependencyExtension_ParentChain
        {
            private DependantOnScene sut = null;

            [SetUp]
            public void SceneSetUp()
            {
                this.sut = this.parentChain.Self.AddComponent<DependantOnScene>();
            }

            [Test]
            public void Capture_CandidateInSelf_IsCaptured()
            {
                var candidate = this.parentChain.Self.AddComponent<CapsuleCollider>();
                this.sut.CaptureDependencies();

                Assert.AreSame(sut.dependency, candidate);
            }

            [Test]
            public void Capture_CandidateInGrandchild_IsCaptured()
            {
                var candidate = this.parentChain.Grandchild.AddComponent<CapsuleCollider>();
                this.sut.CaptureDependencies();

                Assert.AreSame(sut.dependency, candidate);
            }

            [Test]
            public void Capture_CandidateInChild_IsCaptured()
            {
                var candidate = this.parentChain.Child.AddComponent<CapsuleCollider>();
                this.sut.CaptureDependencies();

                Assert.AreSame(sut.dependency, candidate);
            }

            [Test]
            public void Capture_CandidateInParent_IsCaptured()
            {
                var candidate = this.parentChain.Parent.AddComponent<CapsuleCollider>();
                this.sut.CaptureDependencies();

                Assert.AreSame(this.sut.dependency, candidate);
            }

            [Test]
            public void Capture_CandidateInGrandparent_IsCaptured()
            {
                var candidate = this.parentChain.Grandparent.AddComponent<CapsuleCollider>();
                this.sut.CaptureDependencies();

                Assert.AreSame(this.sut.dependency, candidate);
            }

            [Test]
            public void Capture_CandidateInScene_IsCaptured()
            {
                var candidate = this.goBuilder.CreateInRoot<CapsuleCollider>("Candidate");
                this.sut.CaptureDependencies();

                Assert.AreSame(this.sut.dependency, candidate);
            }

            public class DependantOnScene : MonoBehaviour
            {
                [Dependency(Scope.Scene)]
                public CapsuleCollider dependency = null;
            }
        }
    }
}
