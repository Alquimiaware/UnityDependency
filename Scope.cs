namespace Alquimiaware
{
    /// <summary>
    /// The scope to search on
    /// </summary>
    public enum Scope
    {
        /// <summary>
        /// The subtree from the object ( including it )
        /// </summary>
        Subtree,

        /// <summary>
        /// Any of the parents up to the root.
        /// </summary>
        Ancestor,

        /// <summary>
        /// The scene
        /// </summary>
        Scene
    } 
}