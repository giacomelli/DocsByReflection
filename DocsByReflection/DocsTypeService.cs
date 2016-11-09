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
        /// <param name="isOut">Whether the declaring member for this type is an out directional parameter.</param>
        /// <param name="isMethodParameter">If the type is being used has a method parameter.</param>
        /// <returns>The full name.</returns>
        public static string GetTypeFullNameForXmlDoc(Type type, bool isOut = false, bool isMethodParameter = false)
        {
            //JA: 11.8.2016 Generic parameter type handling
            if (type.Name.Equals("T"))
                return "``0";
            Type[] args = type.GetGenericArguments();
            string fullTypeName = string.Empty;
            string typeNamespace = type.Namespace == null ? "" : string.Format("{0}.", type.Namespace);
            if (type.MemberType == MemberTypes.TypeInfo && (type.IsGenericType || args.Length > 0) && (!type.IsClass || isMethodParameter))
            {
                //2016-10-06 by Jeffrey, support multiple generic arguments
                return String.Format(CultureInfo.InvariantCulture,
                     "{0}{1}{{{2}}}{3}",
                     typeNamespace,
                     //type.Name.Replace("`1", ""),
                     System.Text.RegularExpressions.Regex.Replace(type.Name, "`[0-9]+", ""),
                     string.Join(",",
                         //JA: 11.8.2016 Nested parameters will never be out parameters but still need to maintain the flag for isMethodParameter
                         args.Select(o => GetTypeFullNameForXmlDoc(o, false, isMethodParameter)).ToArray()), isOut ? "@" : "").Replace("&", "");
                    //GetTypeFullNameForXmlDoc(type.GetGenericArguments().FirstOrDefault()));
            }
            else if (type.IsNested)
            {
                return String.Format(CultureInfo.InvariantCulture, "{0}{1}.{2}{3}", typeNamespace, type.DeclaringType.Name, type.Name, isOut ? "@" : "").Replace("&", "");
            }
            else
            {
                return String.Format(CultureInfo.InvariantCulture, "{0}{1}{2}", typeNamespace, type.Name, isOut ? "@" : "").Replace("&", "");
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
                    if (xmlElement.NodeType == XmlNodeType.Comment)
                        continue;
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
