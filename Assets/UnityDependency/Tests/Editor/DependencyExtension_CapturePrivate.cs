namespace UnityDependency.Tests
{
    using Alquimiaware;
    using NUnit.Framework;
    using UnityEngine;

    [TestFixture]
    public class DependencyExtension_CapturePrivate
    {
        private PrivateDependency sut = null;

        [TearDown]
        public void TearDown()
        {
            GameObject.DestroyImmediate(this.sut.gameObject);
        }

        [Test]
        public void Capture_SubtreeDependencyIsPrivate_IsCaptured()
        {
            this.sut = new GameObject("SUT").AddComponent<PrivateDependencyOnSubtree>();
            var expected = this.sut.gameObject.AddComponent<CapsuleCollider>();
            this.sut.CaptureDependencies();

            Assert.AreSame(expected, this.sut.Dependency);
        }

        [Test]
        public void Capture_AncestorDependencyIsPrivate_IsCaptured()
        {
            this.sut = new GameObject("SUT").AddComponent<PrivateDependencyOnAncestor>();
            var expected = this.sut.gameObject.AddComponent<CapsuleCollider>();
            this.sut.CaptureDependencies();

            Assert.AreSame(expected, this.sut.Dependency);
        }

        [Test]
        public void Capture_SceneDependencyIsPrivate_IsCaptured()
        {
            this.sut = new GameObject("SUT").AddComponent<PrivateDependencyOnScene>();
            var expected = this.sut.gameObject.AddComponent<CapsuleCollider>();
            this.sut.CaptureDependencies();

            Assert.AreSame(expected, this.sut.Dependency);
        }

        public class PrivateDependencyOnSubtree : PrivateDependency
        {
            [SerializeField]
            [Dependency(Scope.Subtree)]
            private CapsuleCollider dependency = null;
            public override CapsuleCollider Dependency { get { return this.dependency; } }
        }

        public class PrivateDependencyOnAncestor : PrivateDependency
        {
            [SerializeField]
            [Dependency(Scope.Ancestor)]
            private CapsuleCollider dependency = null;
            public override CapsuleCollider Dependency { get { return this.dependency; } }
        }

        public class PrivateDependencyOnScene : PrivateDependency
        {
            [SerializeField]
            [Dependency(Scope.Scene)]
            private CapsuleCollider dependency = null;
            public override CapsuleCollider Dependency { get { return this.dependency; } }
        }

        public abstract class PrivateDependency : MonoBehaviour
        {
            public abstract CapsuleCollider Dependency { get; }
        }
    }
}
