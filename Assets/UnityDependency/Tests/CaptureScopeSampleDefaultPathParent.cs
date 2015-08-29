namespace UnityDependency.Test
{
    using Alquimiaware;
    using UnityEngine;

    public class CaptureScopeSampleDefaultPathParent : MonoBehaviour
    {
        [SerializeField]
        [Dependency(Scope.Scene, DefaultPath = "..")]
        private BoxCollider parentCollider = null;
        public BoxCollider ParentCollider
        {
            get { return this.parentCollider; }
            set { this.parentCollider = value; }
        }
    }
}
