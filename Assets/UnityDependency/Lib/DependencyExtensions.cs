namespace Alquimiaware
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using UnityEngine;

    public static class DependencyExtensions
    {
        static DependencyExtensions()
        {
            ErrorEmissor = new UnityErrorEmissor();
        }

        private static void NotifyError(string message)
        {
            if (ErrorEmissor != null)
                ErrorEmissor.NotifyError(message);
        }

        private static void NotifyErrorFormat(string message, params object[] args)
        {
            if (ErrorEmissor != null)
            {
                string formattedMessage = string.Format(message, args);
                ErrorEmissor.NotifyError(formattedMessage);
            }
        }

        private const string FindDependencyExtensionName = "FindClosestComponent";
        private const string AddComponentMethodName = "AddComponent";
        private const string selfJumper = ".";
        private const string parentJumper = "..";

        public static void ForceRecaptureDependencies(this MonoBehaviour monoBehaviour)
        {
            var fieldInfos = GetFields(monoBehaviour);

            foreach (var fi in fieldInfos)
            {
                var dependency = GetDependencyAttribute(fi);
                if (dependency != null)
                    fi.SetValue(monoBehaviour, null);
            }

            CaptureDependencies(monoBehaviour);
        }

        public static void CaptureDependencies(this MonoBehaviour monoBehaviour)
        {
            var fieldInfos = GetFields(monoBehaviour);


            // Try capture all
            foreach (var fi in fieldInfos)
            {
                var dependency = GetDependencyAttribute(fi);
                if (dependency != null)
                {
                    // get current value, if is null, do nothing
                    var value = fi.GetValue(monoBehaviour);
                    if (DependencyExtensions.IsValueDefined(value))
                        continue;

                    var findDependencyMethod = typeof(DependencyExtensions).GetMethod(FindDependencyExtensionName);
                    var specifiedMethod = findDependencyMethod.MakeGenericMethod(fi.FieldType);
                    // if is not null capture a dependency
                    value = specifiedMethod.Invoke(null, new object[]
                    {
                        monoBehaviour, 
                        dependency.SearchScope, 
                        null
                    });

                    if (DependencyExtensions.IsValueDefined(value))
                        fi.SetValue(monoBehaviour, value);
                }
            }

            // Method to use to add a component of a given type
            var specificAddComp = typeof(GameObject).GetMethod(
                AddComponentMethodName,
                new Type[] { typeof(Type) }
                );

            // Generate defaults for not found dependencies
            foreach (var fi in fieldInfos)
            {
                var value = fi.GetValue(monoBehaviour);
                if (DependencyExtensions.IsValueDefined(value))
                    continue;

                var dependency = GetDependencyAttribute(fi);
                if (dependency == null)
                    continue;

                var defaultType = dependency.DefaultType ?? fi.FieldType;

                if (defaultType.IsAbstract)
                {
                    NotifyErrorFormat("Field '{1}' in class '{0}' must have a non-abstract DefaultType. Current DefaultType is '{2}'.",
                                      fi.DeclaringType.Name,
                                      fi.Name,
                                      defaultType.Name);
                    continue;
                }

                Transform targetNode = null;

                if (dependency.DefaultPath == null || string.IsNullOrEmpty(dependency.DefaultPath.Trim()))
                {
                    targetNode = monoBehaviour.transform;
                }
                else
                {
                    var segments = dependency.DefaultPath.Trim().Split('/');
                    if (segments.Length == 2
                        && string.IsNullOrEmpty(segments[0])
                        && string.IsNullOrEmpty(segments[1]))
                        throw new System.ArgumentOutOfRangeException("DefaultPath", "'" + dependency.DefaultPath + "' is not a valid path.");

                    if (IsRelativePath(segments))
                    {
                        targetNode = NavigatePath(monoBehaviour.transform, segments, 0);
                    }
                    else
                    {
                        int skipped = 1;
                        string firstName = segments[0].Trim();

                        if (string.IsNullOrEmpty(firstName))
                        {
                            firstName = segments[1].Trim();
                            skipped = 2;
                        }

                        var startGO = GameObject.Find("/" + firstName);
                        if(startGO == null)
                            startGO = new GameObject(firstName);

                        targetNode = NavigatePath(startGO.transform, segments, skipped);
                    }
                }

                value = specificAddComp.Invoke(
                    targetNode.gameObject,
                    new object[] { defaultType });

                fi.SetValue(monoBehaviour, value);
            }
        }

        private static bool IsRelativePath(string[] segments)
        {
            string first = segments[0].Trim();

            return first == selfJumper
                || first == parentJumper
                || (segments.Length == 1 && string.IsNullOrEmpty(first));
        }

        private static Transform NavigatePath(Transform startNode, string[] segments, int startIndex)
        {
            Transform currentNode = startNode;

            for (int i = startIndex; i < segments.Length; ++i)
            {
                string segment = segments[i].Trim();

                if (string.IsNullOrEmpty(segment))
                    throw new System.ArgumentOutOfRangeException("DefaultPath", string.Format("{0} cannot have a child with empty name.", currentNode.name));

                if (segment == selfJumper)
                    continue;

                if (segment == parentJumper)
                {
                    if (currentNode.parent != null)
                    {
                        currentNode = currentNode.parent;
                    }
                    else
                    {
                        string message = string.Format("{0} has no parent", currentNode.name);
                        throw new ArgumentOutOfRangeException("DefaultPath", message);
                    }
                }
                else
                {
                    currentNode = currentNode.GetOrAddChild(segment);
                }
            }

            return currentNode;
        }

        /// <summary>
        /// Determine if a value is defined, covering Unity's corner cases.
        /// Unity objects, if their assignment is missing, return a string literal @"null", instead of <code>null</code>.
        /// </summary>
        /// <param name="value">Value to check.</param>
        /// <returns><code>true</code> if the value is properly assigned; <code>false</code> otherwise.</returns>
        private static bool IsValueDefined(object value)
        {
            if (value is UnityEngine.Object)
                return ((UnityEngine.Object)value);

            return value != null;
        }

        public static bool HasDependencies(this MonoBehaviour monoBehaviour)
        {
            var fieldInfos = GetFields(monoBehaviour);

            return fieldInfos.Any(fi =>
                fi.GetCustomAttributes(typeof(DependencyAttribute), true).Length > 0);
        }

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
            if (go == null) throw new ArgumentNullException("go");
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
            if (go == null) throw new ArgumentNullException("go");
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

        private static FieldInfo[] GetFields(MonoBehaviour monoBehaviour)
        {
            return GetFields(monoBehaviour.GetType()).ToArray();
        }

        private static IEnumerable<FieldInfo> GetFields(Type type)
        {
            var fieldInfos = type.GetFields(
                BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            Type baseType = type.BaseType;
            if (baseType != null)
                return fieldInfos.Concat(GetFields(baseType));

            return fieldInfos;
        }

        private static DependencyAttribute GetDependencyAttribute(FieldInfo fi)
        {
            var fa = fi.GetCustomAttributes(typeof(DependencyAttribute), true);
            var dependency =
                fa.Any() ?
                fa[0] as DependencyAttribute :
                default(DependencyAttribute);
            return dependency;
        }

        public interface IErrorEmissor
        {
            void NotifyError(string message);
        }

        public static IErrorEmissor ErrorEmissor { get; set; }

        public class UnityErrorEmissor : IErrorEmissor
        {
            public void NotifyError(string message)
            {
                UnityEngine.Debug.LogError(message);
            }
        }
    }
}