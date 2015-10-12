namespace UnityDependency.Tests
{
    using Alquimiaware;
    using NUnit.Framework;
    using UnityEngine;

    [TestFixture]
    public abstract partial class DependencyExtension_ParentChain
    {
        public class DefaultNotDeclared : MonoBehaviour
        {
            [Dependency(Scope.Scene)]
            public CapsuleCollider field = null;
        }

        public class DefaultTypeDeclared : MonoBehaviour
        {
            [Dependency(Scope.Scene, DefaultType = typeof(CapsuleCollider))]
            public Collider field = null;
        }
    }
}
