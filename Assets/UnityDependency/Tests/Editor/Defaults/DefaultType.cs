namespace UnityDependency.Tests
{
    using Alquimiaware;
    using NUnit.Framework;
    using UnityEngine;

    public abstract partial class DependencyExtension_ParentChain
    {
        public class DefaultType : DependencyExtension_ParentChain
        {
            [Test]
            public void Capture_HasNoDefaultType_IsCaptured()
            {
                var sut = this.goBuilder.CreateInRoot<DefaultNotDeclared>();
                sut.CaptureDependencies();

                Assert.IsNotNull(sut.field);
            }

            [Test]
            public void Capture_HasNoDefaultType_CreatesComponentOfFieldType()
            {
                var sut = this.goBuilder.CreateInRoot<DefaultNotDeclared>();
                sut.CaptureDependencies();

                // Is.TypeOf checks exact type, whereas Assert.IsInstanceOf<> checks type compatibility
                Assert.That(sut.field, Is.TypeOf<CapsuleCollider>());
            }

            [Test]
            public void Capture_HasDefaultType_IsCaptured()
            {
                var sut = this.goBuilder.CreateInRoot<DefaultTypeDeclared>();
                sut.CaptureDependencies();

                Assert.IsNotNull(sut.field);
            }

            [Test]
            public void Capture_HasDefaultType_CreatesComponentOfType()
            {
                var sut = this.goBuilder.CreateInRoot<DefaultTypeDeclared>();
                sut.CaptureDependencies();

                Assert.That(sut.field, Is.TypeOf<CapsuleCollider>());
            }
        }
    }
}
