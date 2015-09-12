namespace UnityDependency.Test.CaptureScope
{
    using Alquimiaware;
    using NUnit.Framework;
    using UnityEngine;

    public abstract partial class DependencyExtension_ParentChain
    {
        public class ScopeAncestor : DependencyExtension_ParentChain
        {
            [Test]
            public void Capture_CandidateInSelf_IsCaptured()
            {
                var candidate = this.parentChain.Self.AddComponent<SphereCollider>();
                this.sut.CaptureDependencies();

                Assert.AreSame(sut.AncestorCollider, candidate);
            }

            [Test]
            public void Capture_CandidateInChild_IsCaptured()
            {
                var candidate = this.parentChain.Child.AddComponent<SphereCollider>();
                this.sut.CaptureDependencies();

                Assert.AreSame(sut.AncestorCollider, candidate);
            }

            [Test]
            public void Capture_CandidateInParent_IsCaptured()
            {
                var candidate = this.parentChain.Parent.AddComponent<SphereCollider>();
                this.sut.CaptureDependencies();

                Assert.AreSame(this.sut.AncestorCollider, candidate);
            }

            [Test]
            public void Capture_CandidateInGrandparent_IsCaptured()
            {
                var candidate = this.parentChain.Grandparent.AddComponent<SphereCollider>();
                this.sut.CaptureDependencies();

                Assert.AreSame(this.sut.AncestorCollider, candidate);
            }

            [Test]
            public void Capture_CandidateInScene_IsNotCaptured()
            {
                var candidate = this.goBuilder.CreateInRoot<SphereCollider>("Candidate");
                this.sut.CaptureDependencies();

                Assert.AreNotSame(this.sut.AncestorCollider, candidate);
            }
        }
    }
}
