
namespace DocsByReflection
{
    /// <summary>
    /// Exceptions throw helper.
    /// </summary>
    internal static class ThrowHelper
    {
        /// <summary>
        /// Throws the documentation not found.
        /// </summary>
        /// <exception cref="DocsByReflectionException">Could not find documentation for specified element</exception>
        public static void ThrowDocNotFound()
        {
            throw new DocsByReflectionException("Could not find documentation for specified element");
        }
    }
}
