namespace UnityDependency.Test
{
    using UnityEngine;

    public abstract class TestStub : MonoBehaviour
    {
        /// <summary>
        /// Override to set up the test scene.
        /// </summary>
        protected abstract void SetUp();

        /// <summary>
        /// Override to tear down everything created for the test.
        /// </summary>
        protected abstract void TearDown();

        ///////////////////////
        // Assertion helpers //
        ///////////////////////

        protected void AssertIsSame(Object obj1, Object obj2)
        {
            IntegrationTest.Assert(obj1 == obj2, "Objects '" + obj1.ToString() + "' and '" + obj2.ToString() + "' are not equal.");
        }

        protected void AssertIsOther(Object obj1, Object obj2)
        {
            IntegrationTest.Assert(obj1 != obj2, "Objects '" + obj1.ToString() + "' and '" + obj2.ToString() + "' are not different.");
        }

        protected void AssertContainsComponents(GameObject go, params Component[] comps)
        {
            for (int i = 0; i < comps.Length; ++i)
                IntegrationTest.Assert(go == comps[i].gameObject, "GameObject '" + go.ToString() + "' does not contain Component '" + comps[i].ToString() + "'");
        }

        protected void AssertIsAssigned(Object obj)
        {
            IntegrationTest.Assert(obj, "Object '" + obj.ToString() + "' has no value assigned");
        }

        protected void AssertIsUnassigned(Object obj)
        {
            IntegrationTest.Assert(!obj, "Object '" + obj.ToString() + "' has a value assigned");
        }

        protected void AssertIsSubtree(GameObject go, GameObject otherGO, string hierarchy)
        {
            string[] children = hierarchy.Split(new char[] { '/' }, System.StringSplitOptions.RemoveEmptyEntries);
            Transform refTransform = go.transform;
            for (int i = 0; i < children.Length; ++i)
            {
                refTransform = refTransform.FindChild(children[i]);
                IntegrationTest.Assert(refTransform, "No child '" + children[i] + "' below '" + go.ToString() + "'.");

                if (i == children.Length - 1)
                    IntegrationTest.Assert(refTransform == otherGO.transform);
            }
        }

        protected void AssertIsTree(GameObject go, string hierarchy)
        {
            string[] tree = hierarchy.Split(new char[] { '/' }, System.StringSplitOptions.RemoveEmptyEntries);
            Transform refTransform = go.transform;
            for (int i = tree.Length - 1; i >= 0; --i)
            {
                IntegrationTest.Assert(refTransform.gameObject.name == tree[i], "GameObject '" + go.ToString() + "' is not part of root hierarchy '" + hierarchy + "'.");
                refTransform = refTransform.parent;
            }

            IntegrationTest.Assert(refTransform == null, "GameObject '" + go.ToString() + "' is part of an equivalent hierarchy, but it does not reach the root of the scene.");
        }
    }
}
