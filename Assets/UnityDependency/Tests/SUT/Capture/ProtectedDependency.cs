namespace UnityDependency.Test.SUT.Capture
{
    using Alquimiaware;
    using UnityEngine;

    public class ProtectedDependency : MonoBehaviour
    {
        [SerializeField]
        [Dependency(Scope.Scene)]
        protected BoxCollider field;
        public BoxCollider Field { get { return this.field; } }
    }
}
