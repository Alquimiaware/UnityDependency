namespace UnityDependency.Tests
{
    using Alquimiaware;
    using NUnit.Framework;
    using UnityEngine;

    public abstract partial class DependencyExtension_ParentChain
    {
        public class DefaultPathNone : DependencyExtension_ParentChain
        {
            [Test]
            public void Capture_HasCandidate_CandidateIsCaptured()
            {
                var sut = this.goBuilder.CreateInRoot<DefaultNotDeclared>("Root");
                var candidate = this.goBuilder.CreateInRoot<CapsuleCollider>();
                sut.CaptureDependencies();

                Assert.AreSame(candidate, sut.field);
            }

            [Test]
            public void Capture_HasNoCandidate_IsAddedToSelf()
            {
                var sut = this.goBuilder.CreateInRoot<DefaultNotDeclared>("Root");
                sut.CaptureDependencies();

                Assert.AreSame(sut.gameObject, sut.field.gameObject);
            }
        }
    }
}
