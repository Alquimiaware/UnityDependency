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
        Subtree = 0,

        /// <summary>
        /// Any of the parents up to the root.
        /// </summary>
        Ancestor = 1,

        /// <summary>
        /// The scene
        /// </summary>
        Scene = 2
    }
}