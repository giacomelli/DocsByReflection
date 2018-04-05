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
		/// Gets the assembly document file name from code base.
		/// </summary>
		/// <returns>The assembly document file name from code base.</returns>
		/// <param name="assemblyCodeBase">Assemby code base.</param>
		public static string GetAssemblyDocFileNameFromCodeBase(string assemblyCodeBase)
		{
			if (string.IsNullOrWhiteSpace (assemblyCodeBase)) {
				throw new ArgumentNullException ("assemblyCodeBase");
			}

			var prefix = "file:///";

			if (assemblyCodeBase.StartsWith (prefix, StringComparison.OrdinalIgnoreCase)) {
				var filePath = assemblyCodeBase.Substring (prefix.Length);

				if (Environment.OSVersion.Platform == PlatformID.MacOSX || Environment.OSVersion.Platform == PlatformID.Unix) {
					filePath = "/" + filePath;
				}

				return Path.ChangeExtension (filePath, ".xml");
			}
			else
			{
				throw new DocsByReflectionException(string.Format("Could not ascertain assembly filename from assembly code base '{0}'.", assemblyCodeBase));
			}
		}
		#endregion
	}
}