﻿namespace UnityDependency.Test.Polymorphism
{
    using Alquimiaware;
    using NSubstitute;
    using NUnit.Framework;

    public abstract partial class DependencyExtension_Polymorphism
    {
        public class Default : DependencyExtension_Polymorphism
        {
            [Test]
            public void Default_IsAbstract_IsNotCreated()
            {
                var sut = this.goBuilder.CreateDependAbstractDefaultAbstract();
                sut.CaptureDependencies();

                Assert.IsNull(sut.Abstract);
            }

            [Test]
            public void Default_IsAbstract_ErrorIsLogged()
            {
                var errorMock = Substitute.For<DependencyExtensions.IErrorEmissor>();
                DependencyExtensions.ErrorEmissor = errorMock;
                var sut = this.goBuilder.CreateDependAbstractDefaultAbstract();
                sut.CaptureDependencies();

                errorMock.Received(1)
                         .NotifyError(Arg.Is<string>(s => s.Contains("must have a non-abstract DefaultType")));
            }

            [Test]
            public void Default_IsDerived_IsCreated()
            {
                var sut = this.goBuilder.CreateDependAbstractDefaultDerived();
                sut.CaptureDependencies();

                Assert.IsNotNull(sut.Derived);
            }

            private DependencyExtensions.IErrorEmissor configuredEmissor = null;

            [SetUp]
            public void DefaultSetUp()
            {
                this.configuredEmissor = DependencyExtensions.ErrorEmissor;
            }

            [TearDown]
            public void DefaultTearDown()
            {
                DependencyExtensions.ErrorEmissor = this.configuredEmissor;
                this.configuredEmissor = null;
            }
        }
    }
}
