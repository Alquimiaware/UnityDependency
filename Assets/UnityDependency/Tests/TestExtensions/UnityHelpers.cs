namespace UnityDependency.Test.UnityHelpers
{
    using UnityEngine;

    public static class UnityHelpers
    {
        public static GameObject[] CreateHierarchy(params string[] names)
        {
            GameObject[] created = new GameObject[names.Length];
            for(int i = 0; i < names.Length; ++i)
            {
                created[i] = new GameObject(names[i]);
                if (i > 0)
                    created[i].transform.parent = created[i - 1].transform;
            }

            return created;
        }
    }
}
