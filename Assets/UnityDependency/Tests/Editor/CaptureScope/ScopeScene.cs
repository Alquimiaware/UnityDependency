namespace UnityDependency.Test.CaptureScope
{
    using Alquimiaware;
    using NUnit.Framework;
    using UnityEngine;

    public abstract partial class DependencyExtension_ParentChain
    {
        public class ScopeScene : DependencyExtension_ParentChain
        {
            [Test]
            public void Capture_CandidateInSelf_IsCaptured()
            {
                var candidate = this.parentChain.Self.AddComponent<CapsuleCollider>();

                this.sut.CaptureDependencies();
                Assert.AreSame(sut.SceneCollider, candidate);
            }

            [Test]
            public void Capture_CandidateInChild_IsCaptured()
            {
                var candidate = this.parentChain.Child.AddComponent<CapsuleCollider>();

                this.sut.CaptureDependencies();
                Assert.AreSame(sut.SceneCollider, candidate);
            }

            [Test]
            public void Capture_CandidateInParent_IsCaptured()
            {
                var candidate = this.parentChain.Parent.AddComponent<CapsuleCollider>();

                this.sut.CaptureDependencies();
                Assert.AreSame(this.sut.SceneCollider, candidate);
            }

            [Test]
            public void Capture_CandidateInScene_IsCaptured()
            {
                var candidate = this.goBuilder.CreateInRoot<CapsuleCollider>("Candidate");

                this.sut.CaptureDependencies();
                Assert.AreSame(this.sut.SceneCollider, candidate);
            }
        }
    }
}