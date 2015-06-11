namespace Alquimiaware
{
    using System;
    using System.Linq;
    using UnityEngine;

    public static class DependencyExtensions
    {
        /// <summary>
        /// Gets a component, or add it if not found
        /// </summary>
        public static T GetOrAddComponent<T>(this Component component) where T : Component
        {
            if (component == null) throw new ArgumentNullException("component");
            return component.gameObject.GetOrAddComponent<T>();
        }

        /// <summary>
        /// Gets a component, or add it if not found
        /// </summary>
        public static T GetOrAddComponent<T>(this GameObject go) where T : Component
        {
            if (go == null) throw new ArgumentNullException("go");
            var requestedComponent = go.GetComponent<T>();
            if (requestedComponent == null)
                requestedComponent = go.AddComponent<T>();

            return requestedComponent;
        }

        /// <summary>
        /// Gets a child by name, or create it if not found
        /// </summary>
        public static Transform GetOrAddChild(this GameObject go, string name)
        {
            if (go == null) throw new ArgumentNullException("go");
            if (string.IsNullOrEmpty(name)) throw new ArgumentOutOfRangeException("name", "cannot be empty");

            var child = go.transform.FindChild(name);
            if (child == null)
            {
                child = new GameObject(name).transform;
                child.parent = go.transform;
                child.localPosition = Vector3.zero;
                child.localRotation = Quaternion.identity;
                child.localScale = Vector3.one;
            }

            return child;
        }

        /// <summary>
        /// Gets a child by name, or create it if not found
        /// </summary>
        public static Transform GetOrAddChild(this Component component, string name)
        {
            if (component == null) throw new ArgumentNullException("component");
            if (string.IsNullOrEmpty(name)) throw new ArgumentOutOfRangeException("name", "cannot be empty");

            return component.gameObject.GetOrAddChild(name);
        }

        /// <summary>
        /// Searches for a component up to a given scope.
        /// Prioritizes the most local scope.
        /// Own -> Children -> Ancestor -> Scene
        /// </summary>
        public static T FindClosestComponent<T>(
            this Component component,
            Scope scope,
            T defaultValue = null) where T : Component
        {
            T dependency = component.GetComponent<T>();
            if (dependency == null && scope >= Scope.Subtree)
                dependency = component.GetComponentInChildren<T>();
            if (dependency == null && scope >= Scope.Ancestor)
                dependency = component._GetComponentInParent<T>();
            if (dependency == null && scope >= Scope.Scene)
                dependency = Component.FindObjectOfType<T>();
            if (dependency == null)
                dependency = defaultValue;

            return dependency;
        }

        private static T _GetComponentInParent<T>(this Component component) 
            where T : Component
        {
#if UNITY_4_0 || UNITY_4_0_1 || UNITY_4_1 || UNITY_4_2 || UNITY_4_3
            return component.gameObject._GetComponentInParent<T>();
#else
            return component.GetComponentInParent<T>();
#endif

            
        }

        private static T _GetComponentInParent<T>(this GameObject go)
            where T : Component
        {
            if (go == null) throw new ArgumentNullException("go");

            T t = go.GetComponent<T>();
            if (t != null)
                return t;

            if (go.transform.parent == null)
                return null;
            else
                return go.transform.parent.gameObject._GetComponentInParent<T>();
        }

        /// <summary>
        /// Gets all the components in children exactly at an specific depth
        /// </summary>
        public static T[] GetComponentsInChildrenAtDepth<T>(this Component component, int captureDepth)
            where T : Component
        {
            if (component == null) throw new ArgumentNullException("component");
            if (captureDepth < 1) throw new ArgumentOutOfRangeException("captureDepth", "must be greater than 0");

            return component.gameObject.GetComponentsInChildrenAtDepth<T>(captureDepth);
        }

        /// <summary>
        /// Gets all the components in children exactly at an specific depth
        /// </summary>
        public static T[] GetComponentsInChildrenAtDepth<T>(this GameObject go, int captureDepth)
            where T : Component
        {
            if (captureDepth < 1) throw new ArgumentOutOfRangeException("captureDepth", "must be greater than 0");

            return go.GetComponentsInChildren<T>()
                     .Where(c => c.gameObject != go
                         && go.transform.GetRelativeDepth(c.transform) == captureDepth)
                     .ToArray();
        }

        /// <summary>
        /// Gets all the children up to a specific depth
        /// </summary>
        public static T[] GetComponentsInChildrenUpToDepth<T>(this Component component, int captureDepth)
            where T : Component
        {
            if (component == null) throw new ArgumentNullException("component");
            if (captureDepth < 1) throw new ArgumentOutOfRangeException("captureDepth", "must be greater than 0");

            return component.gameObject.GetComponentsInChildrenUpToDepth<T>(captureDepth);
        }

        /// <summary>
        /// Gets all the children up to a specific depth
        /// </summary>
        public static T[] GetComponentsInChildrenUpToDepth<T>(this GameObject go, int captureDepth)
            where T : Component
        {
            if (captureDepth < 1) throw new ArgumentOutOfRangeException("captureDepth", "must be greater than 0");

            return go.GetComponentsInChildren<T>()
                     .Where(c => c.gameObject != go
                         && go.transform.GetRelativeDepth(c.transform) <= captureDepth)
                     .ToArray();
        }

        /// <summary>
        /// Gets the relative depth of a descendant of an object.
        /// </summary>
        public static int GetRelativeDepth(this Transform relativeTo, Transform other)
        {
            if (relativeTo == null) throw new ArgumentNullException("relativeTo");
            if (other == null) throw new ArgumentNullException("other");

            int depth = 0;
            var parent = other;
            do
            {
                parent = parent.parent;
                depth += 1;
            }
            while (parent != null
                && parent != relativeTo);

            if (parent != relativeTo)
                throw new ArgumentOutOfRangeException(
                    "other",
                    string.Format("'{0}' is not a descendant of '{1}'", other.name, relativeTo.name));

            return depth;
        }
    }
}