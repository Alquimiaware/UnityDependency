namespace UnityDependency.Tests
{
    using Alquimiaware;
    using NUnit.Framework;
    using UnityEngine;

    public abstract partial class DependencyExtensions_ParentChain
    {
        public class DefaultPathRelative : DependencyExtension_ParentChain
        {
            [Test]
            public void Capture_HasCandidate_CandidateIsCaptured()
            {
                var sut = this.parentChain.Self.AddComponent<DefaultPathRelativeSelf>();
                var candidate = this.goBuilder.CreateInRoot<CapsuleCollider>();
                sut.CaptureDependencies();

                Assert.AreSame(candidate, sut.field);
            }

            [Test]
            public void Capture_PathIsSelf_IsAddedToSelf()
            {
                var sut = this.parentChain.Self.AddComponent<DefaultPathRelativeSelf>();
                sut.CaptureDependencies();

                Assert.AreSame(this.parentChain.Self, sut.field.gameObject);
            }

            [Test]
            public void Capture_PathIsEmpty_IsAddedToSelf()
            {
                var sut = this.parentChain.Self.AddComponent<DefaultPathEmpty>();
                sut.CaptureDependencies();

                Assert.AreSame(this.parentChain.Self, sut.field.gameObject);
            }

            [Test]
            public void Capture_PathIsSelfLoopChild_IsAddedToSelf()
            {
                var sut = this.parentChain.Self.AddComponent<DefaultPathRelativeSelfLoopChild>();
                sut.CaptureDependencies();

                Assert.AreSame(this.parentChain.Self, sut.field.gameObject);
            }

            [Test]
            public void Capture_PathIsSelfLoopParent_IsAddedToSelf()
            {
                var sut = this.parentChain.Self.AddComponent<DefaultPathRelativeSelfLoopParent>();
                sut.CaptureDependencies();

                Assert.AreSame(this.parentChain.Self, sut.field.gameObject);
            }

            [Test]
            public void Capture_PathIsExistingChild_IsAdded()
            {
                var sut = this.parentChain.Self.AddComponent<DefaultPathRelativeChild>();
                sut.CaptureDependencies();

                Assert.AreSame(this.parentChain.Child, sut.field.gameObject);
            }

            [Test]
            public void Capture_PathIsMissingChild_IsCreated()
            {
                var sut = this.parentChain.Self.AddComponent<DefaultPathRelativeSecondChild>();
                sut.CaptureDependencies();
                var newChild = GameObject.Find("SecondChild");

                Assert.AreSame(newChild, sut.field.gameObject);
            }

            [Test]
            public void Capture_PathIsMissingChild_IsCreatedBesidesChild()
            {
                var sut = this.parentChain.Self.AddComponent<DefaultPathRelativeSecondChild>();
                sut.CaptureDependencies();
                var newChild = GameObject.Find("SecondChild");
                GameObject newParent = newChild.transform.parent.gameObject;

                Assert.AreSame(this.parentChain.Self, newParent);
            }

            [Test]
            public void Capture_PathIsExistingGrandchild_IsAdded()
            {
                var sut = this.parentChain.Self.AddComponent<DefaultPathRelativeGrandchild>();
                sut.CaptureDependencies();

                Assert.AreSame(this.parentChain.Grandchild, sut.field.gameObject);
            }

            [Test]
            public void Capture_PathIsMissingGrandchild_IsCreated()
            {
                var sut = this.parentChain.Self.AddComponent<DefaultPathRelativeSecondGrandchild>();
                sut.CaptureDependencies();
                var newGrandchild = GameObject.Find("SecondChild/Grandchild");

                Assert.AreSame(newGrandchild, sut.field.gameObject);
            }

            [Test]
            public void Capture_PathIsMissingGrandchild_IsCreatedBesidesGrandchild()
            {
                var sut = this.parentChain.Self.AddComponent<DefaultPathRelativeSecondGrandchild>();
                sut.CaptureDependencies();
                var newGrandchild = GameObject.Find("SecondChild/Grandchild");
                GameObject newGrandparent = newGrandchild.transform.parent.parent.gameObject;

                Assert.AreSame(this.parentChain.Self, newGrandparent);
            }

            [Test]
            public void Capture_PathIsParent_IsAdded()
            {
                var sut = this.parentChain.Self.AddComponent<DefaultPathRelativeParent>();
                sut.CaptureDependencies();

                Assert.AreSame(this.parentChain.Parent, sut.field.gameObject);
            }

            [Test]
            public void Capture_PathIsGrandparent_IsAdded()
            {
                var sut = this.parentChain.Self.AddComponent<DefaultPathRelativeGrandparent>();
                sut.CaptureDependencies();

                Assert.AreSame(this.parentChain.Grandparent, sut.field.gameObject);
            }

            [Test]
            public void Capture_PathIsParentAndSelfIsRoot_Fails()
            {
                var sut = this.goBuilder.CreateInRoot<DefaultPathRelativeParent>("Root");
                var ex = Assert.Catch<System.ArgumentOutOfRangeException>(() => sut.CaptureDependencies());

                StringAssert.Contains("has no parent", ex.Message);
            }

            [Test]
            public void Capture_PathIsGrandparentAndParentIsRoot_Fails()
            {
                var sut = this.parentChain.Parent.AddComponent<DefaultPathRelativeGrandparent>();
                var ex = Assert.Catch<System.ArgumentOutOfRangeException>(() => sut.CaptureDependencies());

                StringAssert.Contains("has no parent", ex.Message);
            }

            [Test]
            public void Capture_PathIsMissingSibling_IsCreated()
            {
                var sut = this.parentChain.Self.AddComponent<DefaultPathRelativeSibling>();
                try
                {
                    sut.CaptureDependencies();
                    var created = GameObject.Find("Sibling");

                    Assert.IsNotNull(sut.field);
                    Assert.AreSame(created, sut.field.gameObject);
                }
                finally
                {
                    GameObject.DestroyImmediate(sut.field.gameObject);
                }
            }

            [Test]
            public void Capture_PathIsMissingSibling_IsCreatedBesidesSelf()
            {
                var sut = this.parentChain.Self.AddComponent<DefaultPathRelativeSibling>();
                try
                {
                    sut.CaptureDependencies();
                    var created = GameObject.Find("Sibling");

                    Assert.AreSame(created.transform.parent, sut.transform.parent);
                }
                finally
                {
                    GameObject.DestroyImmediate(sut.field.gameObject);
                }
            }

            [Test]
            public void Capture_PathIsExistingSibling_IsAdded()
            {
                var sut = this.parentChain.Self.AddComponent<DefaultPathRelativeSibling>();
                var sibling = this.goBuilder.CreateChild(this.parentChain.Parent.transform, "Sibling");
                sut.CaptureDependencies();

                Assert.AreSame(sibling, sut.field.gameObject);
            }
        }
    }
}
