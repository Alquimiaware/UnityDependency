namespace UnityDependency.Tests
{
    using Alquimiaware;
    using NUnit.Framework;
    using UnityEngine;

    public abstract partial class DependencyExtension_ParentChain
    {
        public class DefaultPathAbsolute : DependencyExtension_ParentChain
        {
            [Test]
            public void Capture_PathIsSlash_Fails()
            {
                var sut = this.parentChain.Self.AddComponent<DefaultPathSlash>();
                var ex = Assert.Catch<System.ArgumentOutOfRangeException>(() => sut.CaptureDependencies());

                StringAssert.Contains("not a valid path", ex.Message);
            }

            [Test]
            public void Capture_PathContainsDoubleSlash_Fails()
            {
                var sut = this.parentChain.Self.AddComponent<DefaultPathDoubleSlash>();
                try
                {
                    var ex = Assert.Catch<System.ArgumentOutOfRangeException>(() => sut.CaptureDependencies());

                    StringAssert.Contains("child with empty name", ex.Message);
                }
                finally
                {
                    GameObject.DestroyImmediate(GameObject.Find("/New"));
                }
            }

            [Test]
            public void Capture_PathEndsWithSlash_Fails()
            {
                var sut = this.parentChain.Self.AddComponent<DefaultPathEndingSlash>();
                var ex = Assert.Catch<System.ArgumentOutOfRangeException>(() => sut.CaptureDependencies());

                StringAssert.Contains("child with empty name", ex.Message);
            }

            [Test]
            public void Capture_PathIsSlashPlusExistingRootObject_IsAdded()
            {
                var sut = this.parentChain.Self.AddComponent<DefaultPathStartingSlash>();
                sut.CaptureDependencies();

                Assert.AreSame(this.parentChain.Grandparent, sut.field.gameObject);
            }

            [Test]
            public void Capture_PathIsMissingRootObject_IsCreated()
            {
                var sut = this.parentChain.Self.AddComponent<DefaultPathAbsoluteNew>();
                try
                {
                    sut.CaptureDependencies();
                    var created = GameObject.Find("New");

                    Assert.AreEqual(created, sut.field.gameObject);
                }
                finally
                {
                    GameObject.DestroyImmediate(sut.field.gameObject);
                }
            }

            [Test]
            public void Capture_PathIsExistingRootObject_IsAdded()
            {
                var sut = this.parentChain.Self.AddComponent<DefaultPathAbsoluteGrandparent>();
                sut.CaptureDependencies();

                Assert.AreSame(this.parentChain.Grandparent, sut.field.gameObject);
            }

            [Test]
            public void Capture_PathIsRootLoop_IsAdded()
            {
                var sut = this.parentChain.Self.AddComponent<DefaultPathAbsoluteGrandparentLoop>();
                sut.CaptureDependencies();

                Assert.AreSame(this.parentChain.Grandparent, sut.field.gameObject);
            }

            [Test]
            public void Capture_PathIsMissingRootChild_IsCreated()
            {
                var sut = this.parentChain.Self.AddComponent<DefaultPathAbsoluteUncle>();
                try
                {
                    sut.CaptureDependencies();
                    var created = GameObject.Find("/Grandparent/Uncle");

                    Assert.AreSame(created, sut.field.gameObject);
                }
                finally
                {
                    GameObject.DestroyImmediate(sut.field.gameObject);
                }
            }

            [Test]
            public void Capture_PathIsExistingRootChild_IsAdded()
            {
                var sut = this.parentChain.Self.AddComponent<DefaultPathAbsoluteParent>();
                sut.CaptureDependencies();

                Assert.AreSame(this.parentChain.Parent, sut.field.gameObject);
            }

            [Test]
            public void Capture_PathIsRootParent_Fails()
            {
                var sut = this.parentChain.Self.AddComponent<DefaultPathAbsoluteGrandgrandparent>();
                var ex = Assert.Catch<System.ArgumentOutOfRangeException>(() => sut.CaptureDependencies());

                StringAssert.Contains("has no parent", ex.Message);
            }
        }
    }
}