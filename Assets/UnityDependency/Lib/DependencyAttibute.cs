namespace Alquimiaware
{
    using System;
    using UnityEngine;

    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public sealed class DependencyAttribute : PropertyAttribute
    {
        private readonly Scope searchScope;

        public DependencyAttribute(Scope searchScope)
        {
            this.searchScope = searchScope;
        }

        /// <summary>
        /// The scope to search in.
        /// </summary>
        public Scope SearchScope
        {
            get { return this.searchScope; }
        }

        /// <summary>
        /// The default path to place a default created dependency when not found.
        /// </summary>
        /// <remarks>When omitted, it will be considered self ('./')</remarks>
        public string DefaultPath { get; set; }

        /// <summary>
        /// The type of the dependency to be found.
        /// </summary>
        /// <remarks>When omitted, it will be considered the target field type.</remarks>
        public Type DefaultType { get; set; }
    }

}