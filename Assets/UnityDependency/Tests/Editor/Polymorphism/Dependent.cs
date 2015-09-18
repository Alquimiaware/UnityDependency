namespace UnityDependency.Test.Polymorphism
{
    using Alquimiaware;
    using NUnit.Framework;

    public abstract partial class DependencyExtension_Polymorphism
    {
        public class Dependent : DependencyExtension_Polymorphism
        {
            [Test]
            public void Capture_DependOnBase_IsCaptured()
            {
                var sut = this.goBuilder.CreateDependAbstract();
                var candidate = this.goBuilder.CreateDerived();
                sut.CaptureDependencies();

                Assert.AreSame(sut.Abstract, candidate);
            }

            [Test]
            public void Capture_DependOnDerived_IsCaptured()
            {
                var sut = this.goBuilder.CreateDependDerived();
                var candidate = this.goBuilder.CreateDerived();
                sut.CaptureDependencies();

                Assert.AreSame(sut.Derived, candidate);
            }
        }
    }
}
