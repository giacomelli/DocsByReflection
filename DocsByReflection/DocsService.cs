//Except where stated all code and programs in this project are the copyright of Jim Blackler, 2008.
//jimblackler@gmail.com
//
//This is free software. Libraries and programs are distributed under the terms of the GNU Lesser
//General Public License. Please see the files COPYING and COPYING.LESSER.

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Globalization;
using System.Linq;
using System.Diagnostics.CodeAnalysis;

namespace DocsByReflection
{
    /// <summary>
    /// Utility class to provide documentation for various types where available with the assembly
    /// </summary>
    public static class DocsService
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

		#region Public methods
		/// <summary>
        /// Provides the documentation comments for a specific method
        /// </summary>
        /// <param name="method">The MethodInfo (reflection data ) of the member to find documentation for</param>
        /// <returns>The XML fragment describing the method</returns>
		[SuppressMessage("Microsoft.Design", "CA1059:MembersShouldNotExposeCertainConcreteTypes", MessageId = "System.Xml.XmlNode")]
		public static XmlElement GetXmlFromMember(MethodBase method)
        {
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}

            // Calculate the parameter string as this is in the member name in the XML
            string parametersString = "";

            foreach (ParameterInfo parameterInfo in method.GetParameters())
            {
                if (parametersString.Length > 0)
                {
                    parametersString += ",";
                }

				parametersString += GetTypeFullNameForXmlDoc(parameterInfo.ParameterType, true);
            }

            //AL: 15.04.2008 ==> BUG-FIX remove “()” if parametersString is empty
            if (parametersString.Length > 0)
                return GetXmlFromName(method.DeclaringType, 'M', method.Name + "(" + parametersString + ")");
            else
                return GetXmlFromName(method.DeclaringType, 'M', method.Name);
        }

        /// <summary>
        /// Provides the documentation comments for a specific member.
        /// </summary>
        /// <param name="member">The MemberInfo (reflection data) or the member to find documentation for</param>
        /// <returns>The XML fragment describing the member</returns>
		[SuppressMessage("Microsoft.Design", "CA1059:MembersShouldNotExposeCertainConcreteTypes", MessageId = "System.Xml.XmlNode")]
		public static XmlElement GetXmlFromMember(MemberInfo member)
        {
			if (member == null)
			{
				throw new ArgumentNullException("member");
			}

            // First character [0] of member type is prefix character in the name in the XML
            return GetXmlFromName(member.DeclaringType, member.MemberType.ToString()[0], member.Name);
        }

		/// <summary>
		/// Provides the documentation comments for a specific parameter.
		/// </summary>
		/// <param name="parameter">The ParameterInfo (reflection data) to find documentation for.</param>
		/// <returns>The XML fragment describing the paramter.</returns>
		[SuppressMessage("Microsoft.Design", "CA1059:MembersShouldNotExposeCertainConcreteTypes", MessageId = "System.Xml.XmlNode")]
		public static XmlElement GetXmlFromParameter(ParameterInfo parameter)
		{
			if (parameter == null)
			{
				throw new ArgumentNullException("parameter");
			}

			var method = parameter.Member as MethodBase;
			var memberDoc = method == null ? GetXmlFromMember(parameter.Member) : GetXmlFromMember(method);

			return memberDoc.SelectSingleNode(String.Format(CultureInfo.InvariantCulture, "param[@name='{0}']", parameter.Name)) as XmlElement;
		}

        /// <summary>
        /// Provides the documentation comments for a specific type
        /// </summary>
        /// <param name="type">Type to find the documentation for</param>
        /// <returns>The XML fragment that describes the type</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1059:MembersShouldNotExposeCertainConcreteTypes", MessageId = "System.Xml.XmlNode")]
		public static XmlElement GetXmlFromType(Type type)
        {
            // Prefix in type names is T
            return GetXmlFromName(type, 'T', "");
        }

		/// <summary>
		/// Obtains the documentation file for the specified assembly
		/// </summary>
		/// <param name="assembly">The assembly to find the XML document for</param>
		/// <returns>The XML document</returns>
		/// <remarks>This version uses a cache to preserve the assemblies, so that 
		/// the XML file is not loaded and parsed on every single lookup</remarks>
		[SuppressMessage("Microsoft.Usage", "CA2200:RethrowToPreserveStackDetails"), SuppressMessage("Microsoft.Design", "CA1059:MembersShouldNotExposeCertainConcreteTypes", MessageId = "System.Xml.XmlNode")]
		public static XmlDocument GetXmlFromAssembly(Assembly assembly)
		{
			if (s_failCache.ContainsKey(assembly))
			{
				throw s_failCache[assembly];
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
				throw exception;
			}
		}
		#endregion

		#region Private methods
		/// <summary>
        /// Obtains the XML Element that describes a reflection element by searching the 
        /// members for a member that has a name that describes the element.
        /// </summary>
        /// <param name="type">The type or parent type, used to fetch the assembly</param>
        /// <param name="prefix">The prefix as seen in the name attribute in the documentation XML</param>
        /// <param name="name">Where relevant, the full name qualifier for the element</param>
        /// <returns>The member that has a name that describes the specified reflection element</returns>
        private static XmlElement GetXmlFromName(Type type, char prefix, string name)
        {
			string fullName = GetTypeFullNameForXmlDoc(type);

            if (String.IsNullOrEmpty(name))
            {
				fullName = String.Format(CultureInfo.InvariantCulture, "{0}:{1}", prefix, fullName);
            }
            else
            {
				fullName = String.Format(CultureInfo.InvariantCulture, "{0}:{1}.{2}", prefix, fullName, name);
            }

            XmlDocument xmlDocument = GetXmlFromAssembly(type.Assembly);

            XmlElement matchedElement = null;

            foreach (XmlElement xmlElement in xmlDocument["doc"]["members"])
            {
                if (xmlElement.Attributes["name"].Value.Equals(fullName, StringComparison.OrdinalIgnoreCase))
                {
                    if (matchedElement != null)
                    {
                        throw new DocsByReflectionException("Multiple matches to query", null);
                    }

                    matchedElement = xmlElement;
                }
            }

            if (matchedElement == null)
            {
                throw new DocsByReflectionException("Could not find documentation for specified element", null);
            }

            return matchedElement;
        }          

        /// <summary>
        /// Loads and parses the documentation file for the specified assembly
        /// </summary>
        /// <param name="assembly">The assembly to find the XML document for</param>
        /// <returns>The XML document</returns>
        private static XmlDocument GetXmlFromAssemblyNonCached(Assembly assembly)
        {
            string assemblyFilename = assembly.CodeBase;

            const string prefix = "file:///";

            if (assemblyFilename.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
            {
				var filePath = assemblyFilename.Substring(prefix.Length);;

				if(Environment.OSVersion.Platform == PlatformID.MacOSX || Environment.OSVersion.Platform == PlatformID.Unix)
				{
					filePath = "/" + filePath;
				}

				filePath = Path.ChangeExtension(filePath, ".xml");

                try
                {
					using(var streamReader = new StreamReader(filePath))
					{
						 XmlDocument xmlDocument = new XmlDocument();
						xmlDocument.Load(streamReader);
						return xmlDocument;
					}                    
                }
				catch(DirectoryNotFoundException directoryException)
				{
					var msg = String.Format(CultureInfo.InvariantCulture, "Error trying to locate the XML documentation file on folder {0}.", filePath);
					throw new DocsByReflectionException(msg, directoryException);
				}
                catch (FileNotFoundException exception)
                {
                    throw new DocsByReflectionException("XML documentation not present (make sure it is turned on in project properties when building)", exception);
                }               
            }
            else
            {
                throw new DocsByReflectionException("Could not ascertain assembly filename", null);
            }
		}

		/// <summary>
		/// Gets the type's full name prepared for xml documentation format.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="isMethodParameter">If the type is being used has a method parameter.</param>
		/// <returns>The full name.</returns>
		private static string GetTypeFullNameForXmlDoc(Type type, bool isMethodParameter = false)
		{
			if (type.MemberType == MemberTypes.TypeInfo && type.IsGenericType && (!type.IsClass || isMethodParameter))
			{
				return String.Format(CultureInfo.InvariantCulture, 
					"{0}.{1}{{{2}}}", 
					type.Namespace, 
					type.Name.Replace("`1", ""),
					GetTypeFullNameForXmlDoc(type.GetGenericArguments().FirstOrDefault()));
			}
			else
			{
				return String.Format(CultureInfo.InvariantCulture, "{0}.{1}", type.Namespace, type.Name);
			}
		}
		#endregion		
	}
}
