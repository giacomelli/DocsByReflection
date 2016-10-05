using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace DocsByReflection
{
    /// <summary>
    /// Service to handle type documentations.
    /// </summary>
    internal static class DocsTypeService
    {
        /// <summary>
        /// Gets the type's full name prepared for xml documentation format.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="isMethodParameter">If the type is being used has a method parameter.</param>
        /// <returns>The full name.</returns>
        public static string GetTypeFullNameForXmlDoc(Type type, bool isMethodParameter = false)
        {
            if (type.MemberType == MemberTypes.TypeInfo && type.IsGenericType && (!type.IsClass || isMethodParameter))
            {
                //2016-10-06 by Jeffrey, support multiple generic arguments
                return String.Format(CultureInfo.InvariantCulture,
                     "{0}.{1}{{{2}}}",
                     type.Namespace,
                     //type.Name.Replace("`1", ""),
                     System.Text.RegularExpressions.Regex.Replace(type.Name, "`[0-9]+", ""),
                     string.Join(",",
                         type.GetGenericArguments()
                         .Select(o => GetTypeFullNameForXmlDoc(o)).ToArray()));
                    //GetTypeFullNameForXmlDoc(type.GetGenericArguments().FirstOrDefault()));
            }
            else if (type.IsNested)
            {
                return String.Format(CultureInfo.InvariantCulture, "{0}.{1}.{2}", type.Namespace, type.DeclaringType.Name, type.Name);
            }
            else
            {
                return String.Format(CultureInfo.InvariantCulture, "{0}.{1}", type.Namespace, type.Name);
            }
        }

        /// <summary>
        /// Obtains the XML Element that describes a reflection element by searching the 
        /// members for a member that has a name that describes the element.
        /// </summary>
        /// <param name="type">The type or parent type, used to fetch the assembly</param>
        /// <param name="prefix">The prefix as seen in the name attribute in the documentation XML</param>
        /// <param name="name">Where relevant, the full name qualifier for the element</param>
        /// <param name="throwError">If should throw error when documentation is not found. Default is true.</param>
        /// <returns>The member that has a name that describes the specified reflection element</returns>
        public static XmlElement GetXmlFromName(Type type, char prefix, string name, bool throwError)
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

            XmlDocument xmlDocument = DocsService.GetXmlFromAssembly(type.Assembly, throwError);
            XmlElement matchedElement = null;

            if (xmlDocument != null)
            {
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
            }

            if (matchedElement == null && throwError)
            {
                ThrowHelper.ThrowDocNotFound();
            }

            return matchedElement;
        }
    }
}
