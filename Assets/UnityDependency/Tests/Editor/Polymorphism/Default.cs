namespace UnityDependency.Test.Polymorphism
{
    using Alquimiaware;
    using NUnit.Framework;

    public partial class DependencyExtension_Polymorphism
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
            public void Default_IsDerived_IsCreated()
            {
                var sut = this.goBuilder.CreateDependAbstractDefaultDerived();
                sut.CaptureDependencies();

                Assert.IsNotNull(sut.Derived);
            }
        }
    }
}
