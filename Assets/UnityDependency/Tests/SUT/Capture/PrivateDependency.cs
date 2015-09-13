namespace UnityDependency.Test.SUT.Capture
{
    using Alquimiaware;
    using UnityEngine;

    public class PrivateDependency : MonoBehaviour
    {
        [SerializeField]
        [Dependency(Scope.Scene)]
        private BoxCollider field;
        public BoxCollider Field { get { return this.field; } }
    }
}
