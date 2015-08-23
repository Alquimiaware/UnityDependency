namespace UnityDependency.Test.Polymorphism
{
    using Alquimiaware;
    using UnityEngine;

    public class BaseContainer : MonoBehaviour
    {
        [SerializeField]
        [Dependency(Scope.Scene)]
        private AbstractBaseClass baseInstance = null;
        public AbstractBaseClass BaseInstance { get { return this.baseInstance; } }
    }
}
