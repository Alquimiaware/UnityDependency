namespace UnityDependency.Test.SUT.Capture
{
    using Alquimiaware;
    using UnityEngine;

    public class PublicDependency : MonoBehaviour
    {
        [SerializeField]
        [Dependency(Scope.Scene)]
        public BoxCollider field;
        public BoxCollider Field { get { return this.field; } }
    }
}
