namespace UnityDependency.Test.CaptureScope
{
    using Alquimiaware;
    using NUnit.Framework;
    using UnityEngine;

    public abstract partial class DependencyExtension_ParentChain
    {
        public class ScopeSubtree : DependencyExtension_ParentChain
        {
            [Test]
            public void Capture_CandidateInSelf_IsCaptured()
            {
                var candidate = this.parentChain.Self.AddComponent<BoxCollider>();
                this.sut.CaptureDependencies();

                Assert.AreSame(sut.subtreeCollider, candidate);
            }

            [Test]
            public void Capture_CandidateInChild_IsCaptured()
            {
                var candidate = this.parentChain.Child.AddComponent<BoxCollider>();
                this.sut.CaptureDependencies();

                Assert.AreSame(sut.subtreeCollider, candidate);
            }

            [Test]
            public void Capture_CandidateInGrandchild_IsCaptured()
            {
                var candidate = this.parentChain.Grandchild.AddComponent<BoxCollider>();
                this.sut.CaptureDependencies();

                Assert.AreSame(sut.subtreeCollider, candidate);
            }

            [Test]
            public void Capture_CandidateInParent_IsNotCaptured()
            {
                var candidate = this.parentChain.Parent.AddComponent<BoxCollider>();
                this.sut.CaptureDependencies();

                Assert.AreNotSame(sut.subtreeCollider, candidate);
            }

            [Test]
            public void Capture_CandidateInScene_IsNotCaptured()
            {
                var candidate = this.goBuilder.CreateInRoot<BoxCollider>("Candidate");
                this.sut.CaptureDependencies();

                Assert.AreNotSame(sut.subtreeCollider, candidate);
            }
        }
    }
}
