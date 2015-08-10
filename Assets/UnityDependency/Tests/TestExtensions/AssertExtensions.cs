namespace UnityDependency.Test.AssertExtensions
{
    using UnityEngine;

    public static class AssertExtensions
    {
        public static void AssertIsSame(this Object obj1, Object obj2)
        {
            IntegrationTest.Assert(obj1 == obj2, "Objects '" + obj1.ToString() + "' and '" + obj2.ToString() + "' are not equal.");
        }

        public static void AssertIsOther(this Object obj1, Object obj2)
        {
            IntegrationTest.Assert(obj1 != obj2, "Objects '" + obj1.ToString() + "' and '" + obj2.ToString() + "' are not different.");
        }

        public static void AssertContainsComponents(this GameObject go, params Component[] comps)
        {
            for (int i = 0; i < comps.Length; ++i)
                IntegrationTest.Assert(go == comps[i].gameObject, "GameObject '" + go.ToString() + "' does not contain Component '" + comps[i].ToString() + "'");
        }

        public static void AssertIsAssigned(this Object obj)
        {
            IntegrationTest.Assert(obj, "Object '" + obj.ToString() + "' has no value assigned");
        }

        public static void AssertIsUnassigned(this Object obj)
        {
            IntegrationTest.Assert(!obj, "Object '" + obj.ToString() + "' has a value assigned");
        }

        public static void AssertIsSubtree(this GameObject go, GameObject otherGO, string hierarchy)
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

        public static void AssertIsTree(this GameObject go, string hierarchy)
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
