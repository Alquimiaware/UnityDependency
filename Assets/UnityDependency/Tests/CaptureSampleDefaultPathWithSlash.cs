namespace UnityDependency.Test
{
    using UnityEngine;
    using Alquimiaware;

    public class CaptureSampleDefaultPathWithSlash : MonoBehaviour
    {
        [SerializeField]
        [Dependency(Scope.Scene, DefaultPath = "/Root")]
        private BoxCollider rootCollider = null;
        public BoxCollider RootCollider
        {
            get { return this.rootCollider; }
            set { this.rootCollider = value; }
        }
    }
}
