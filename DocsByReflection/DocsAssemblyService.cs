using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Xml;

namespace DocsByReflection
{
    /// <summary>
    /// Service to handle assembly documentation stuffs.
    /// </summary>
    internal static class DocsAssemblyService
    {
        #region Fields
        /// <summary>
        /// A cache used to remember Xml documentation for assemblies
        /// </summary>
        private static Dictionary<Assembly, XmlDocument> s_cache = new Dictionary<Assembly, XmlDocument>();

        /// <summary>
        /// A cache used to store failure exceptions for assembly lookups
        /// </summary>
        private static Dictionary<Assembly, Exception> s_failCache = new Dictionary<Assembly, Exception>();
        #endregion

        #region Methods
        /// <summary>
        /// Obtains the documentation file for the specified assembly
        /// </summary>
        /// <param name="assembly">The assembly to find the XML document for</param>
        /// <param name="throwError">If should throw error when documentation is not found. Default is true.</param>
        /// <returns>The XML document</returns>
        /// <remarks>This version uses a cache to preserve the assemblies, so that 
        /// the XML file is not loaded and parsed on every single lookup</remarks>
        [SuppressMessage("Microsoft.Usage", "CA2200:RethrowToPreserveStackDetails"), SuppressMessage("Microsoft.Design", "CA1059:MembersShouldNotExposeCertainConcreteTypes", MessageId = "System.Xml.XmlNode")]
        public static XmlDocument GetXmlFromAssembly(Assembly assembly, bool throwError = true)
        {
            if (s_failCache.ContainsKey(assembly))
            {
                if (throwError)
                {
                    throw s_failCache[assembly];
                }

                return null;
            }

            try
            {
                if (!s_cache.ContainsKey(assembly))
                {
                    // load the docuemnt into the cache
                    s_cache[assembly] = GetXmlFromAssemblyNonCached(assembly);
                }

                return s_cache[assembly];
            }
            catch (Exception exception)
            {
                s_failCache[assembly] = exception;

                if (throwError)
                {
                    throw exception;
                }
            }

            return null;
        }

        /// <summary>
        /// Loads and parses the documentation file for the specified assembly
        /// </summary>
        /// <param name="assembly">The assembly to find the XML document for</param>
        /// <returns>The XML document</returns>
        private static XmlDocument GetXmlFromAssemblyNonCached(Assembly assembly)
        {
            string filePath = PathHelper.GetAssemblyDocFileNameFromCodeBase(assembly.CodeBase);

            try
            {
                using (var streamReader = new StreamReader(filePath))
                {
                    var xmlDocument = new XmlDocument();
                    xmlDocument.Load(streamReader);
                    return xmlDocument;
                }
            }
            catch (DirectoryNotFoundException directoryException)
            {
                var msg = string.Format(CultureInfo.InvariantCulture, "Error trying to locate the XML documentation file on folder {0}.", filePath);
                throw new DocsByReflectionException(msg, directoryException);
            }
            catch (FileNotFoundException exception)
            {
                throw new DocsByReflectionException("XML documentation not present (make sure it is turned on in project properties when building)", exception);
            }
            catch (Exception ex)
            {
                throw new DocsByReflectionException(string.Format("Error trying to get documentation filer for assembly code base '{0}'.", assembly.CodeBase), ex);
            }
        }
        #endregion
    }
}
