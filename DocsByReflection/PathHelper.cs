using System;
using System.IO;

namespace DocsByReflection
{
	/// <summary>
	/// Internal path helper.
	/// </summary>
	public static class PathHelper
	{
        #region Methods
        /// <summary>
        /// Gets the assembly document file name from location.
        /// </summary>
        /// <returns>The assembly document file name from location.</returns>
        /// <param name="assemblyLocation">Assemby location.</param>
        public static string GetAssemblyDocFileNameFromLocation(string assemblyLocation)
		{
			if (string.IsNullOrWhiteSpace (assemblyLocation)) {
				throw new ArgumentNullException (nameof(assemblyLocation));
			}

			return Path.ChangeExtension (assemblyLocation, ".xml");
		}
		#endregion
	}
}