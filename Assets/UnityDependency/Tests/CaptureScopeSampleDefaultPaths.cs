namespace UnityDependency.Test
{
    using Alquimiaware;
    using UnityEngine;

    public class CaptureScopeSampleDefaultPaths : MonoBehaviour
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

        [SerializeField]
        [Dependency(Scope.Subtree, DefaultPath = "../../Parent/./Sibling")]
        private CapsuleCollider siblingCollider = null;
        public CapsuleCollider SiblingCollider
        {
            get { return this.siblingCollider; }
            set { this.siblingCollider = value; }
        }

        void Reset()
        {
            this.CaptureDependencies();
        }
    }
}
