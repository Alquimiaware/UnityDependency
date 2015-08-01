namespace Alquimiaware.Editor
{
    using System.Reflection;
    using UnityEditor;
    using UnityEngine;

    public static class DependencyCaptureContextMenuItems
    {
		// NOTE(rafa): The A before the command naming is there to prevent
		// collision with the previous context menus, once migration is completed we can rename them
	
        [MenuItem("CONTEXT/MonoBehaviour/ACapture Dependencies")]
        public static void CaptureDependencies(MenuCommand menuCommand)
        {
            var owner = menuCommand.context as MonoBehaviour;

            CallResetIfDefined(owner);
            owner.CaptureDependencies();
        }

        [MenuItem("CONTEXT/MonoBehaviour/AForce Recapture Dependencies")]
        public static void ForceRecaptureDependencies(MenuCommand menuCommand)
        {
            var owner = menuCommand.context as MonoBehaviour;
            CallResetIfDefined(owner);
            owner.ForceRecaptureDependencies();
        }

        [MenuItem("CONTEXT/MonoBehaviour/ACapture Dependencies", true)]
        public static bool CanCaptureDependencies1(MenuCommand menuCommand)
        {
            var owner = menuCommand.context as MonoBehaviour;
            return owner.HasDependencies();
        }

        [MenuItem("CONTEXT/MonoBehaviour/AForce Recapture Dependencies", true)]
        public static bool CanCaptureDependencies(MenuCommand menuCommand)
        {
            var owner = menuCommand.context as MonoBehaviour;
            return owner.HasDependencies();
        }

        private static void CallResetIfDefined(MonoBehaviour owner)
        {
            var resetMethod = owner.GetType().GetMethod(
                "Reset",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            if (resetMethod != null)
                resetMethod.Invoke(owner, null);
        }
    }
}