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

        public class DefaultPathEmpty : MonoBehaviour
        {
            [Dependency(Scope.Scene, DefaultPath = "")]
            public CapsuleCollider field = null;
        }

        public class DefaultPathRelativeSelf : MonoBehaviour
        {
            [Dependency(Scope.Scene, DefaultPath = ".")]
            public CapsuleCollider field = null;
        }

        public class DefaultPathRelativeSelfLoopParent : MonoBehaviour
        {
            [Dependency(Scope.Scene, DefaultPath = "../Self")]
            public CapsuleCollider field = null;
        }

        public class DefaultPathRelativeSelfLoopChild : MonoBehaviour
        {
            [Dependency(Scope.Scene, DefaultPath = "./Child/..")]
            public CapsuleCollider field = null;
        }

        public class DefaultPathRelativeChild : MonoBehaviour
        {
            [Dependency(Scope.Scene, DefaultPath = "./Child")]
            public CapsuleCollider field = null;
        }

        public class DefaultPathRelativeSecondChild : MonoBehaviour
        {
            [Dependency(Scope.Scene, DefaultPath = "./SecondChild")]
            public CapsuleCollider field = null;
        }

        public class DefaultPathRelativeGrandchild : MonoBehaviour
        {
            [Dependency(Scope.Scene, DefaultPath = "./Child/Grandchild")]
            public CapsuleCollider field = null;
        }

        public class DefaultPathRelativeSecondGrandchild : MonoBehaviour
        {
            [Dependency(Scope.Scene, DefaultPath = "./SecondChild/Grandchild")]
            public CapsuleCollider field = null;
        }

        public class DefaultPathRelativeParent : MonoBehaviour
        {
            [Dependency(Scope.Scene, DefaultPath = "..")]
            public CapsuleCollider field = null;
        }

        public class DefaultPathRelativeGrandparent : MonoBehaviour
        {
            [Dependency(Scope.Scene, DefaultPath = "../..")]
            public CapsuleCollider field = null;
        }

        public class DefaultPathRelativeSibling : MonoBehaviour
        {
            [Dependency(Scope.Scene, DefaultPath = "../Sibling")]
            public CapsuleCollider field = null;
        }

        public class DefaultPathAbsoluteNew : MonoBehaviour
        {
            [Dependency(Scope.Scene, DefaultPath = "New")]
            public CapsuleCollider field = null;
        }

        public class DefaultPathAbsoluteGrandparent : MonoBehaviour
        {
            [Dependency(Scope.Scene, DefaultPath = "Grandparent")]
            public CapsuleCollider field = null;
        }

        public class DefaultPathAbsoluteGrandparentLoop : MonoBehaviour
        {
            [Dependency(Scope.Scene, DefaultPath = "Grandparent/Parent/..")]
            public CapsuleCollider field = null;
        }

        public class DefaultPathAbsoluteGrandgrandparent : MonoBehaviour
        {
            [Dependency(Scope.Scene, DefaultPath = "Grandparent/..")]
            public CapsuleCollider field = null;
        }

        public class DefaultPathAbsoluteParent : MonoBehaviour
        {
            [Dependency(Scope.Scene, DefaultPath = "Grandparent/Parent")]
            public CapsuleCollider field = null;
        }

        public class DefaultPathAbsoluteUncle : MonoBehaviour
        {
            [Dependency(Scope.Scene, DefaultPath = "Grandparent/Uncle")]
            public CapsuleCollider field = null;
        }

    }
}
