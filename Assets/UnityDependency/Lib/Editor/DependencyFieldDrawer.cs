namespace Alquimiaware.Editor
{
    using System.Reflection;
    using UnityEditor;
    using UnityEngine;

    [CustomPropertyDrawer(typeof(DependencyAttribute), true)]
    public class DependencyFieldDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var dependency = (DependencyAttribute)this.attribute;

            var name = string.Format("{0} (D:{1})", label.text, dependency.SearchScope);

            EditorGUI.PropertyField(position, property, new GUIContent(name), true);
        }
    }
}