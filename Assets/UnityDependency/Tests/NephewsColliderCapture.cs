namespace UnityDependency.Test
{
    using Alquimiaware;
    using UnityEngine;

    public class NephewsColliderCapture : MonoBehaviour
    {
        [SerializeField]
        [Dependency(Scope.Subtree, DefaultPath = "./Child/Nephew")]
        public BoxCollider nephewCollider = null;

        [SerializeField]
        [Dependency(Scope.Subtree, DefaultPath = "Root/Child")]
        private SphereCollider rootChildCollider = null;
        public SphereCollider RootChildCollider
        {
            get { return this.rootChildCollider; }
            set { this.rootChildCollider = value; }
        }

        void Reset()
        {
            this.CaptureDependencies();
        }
    }
}
