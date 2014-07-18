
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Xml;
using HelperSharp;
namespace DocsByReflection
{
    /// <summary>
    /// Service to handle method documentations.
    /// </summary>
    internal static class DocsMethodService
    {
        /// <summary>
        /// Provides the documentation comments for a specific method.
        /// </summary>
        /// <param name="method">The MethodInfo (reflection data ) of the member to find documentation for</param>
        /// <param name="throwError">If should throw error when documentation is not found. Default is true.</param>
        /// <returns>The XML fragment describing the method</returns>
        [SuppressMessage("Microsoft.Design", "CA1059:MembersShouldNotExposeCertainConcreteTypes", MessageId = "System.Xml.XmlNode")]
        public static XmlElement GetXmlFromMember(MethodBase method, bool throwError = true)
        {
            ExceptionHelper.ThrowIfNull("method", method);
            
            // Calculate the parameter string as this is in the member name in the XML
            var parameters = new List<string>();

            foreach (var parameterInfo in method.GetParameters())
            {
                parameters.Add(DocsTypeService.GetTypeFullNameForXmlDoc(parameterInfo.ParameterType, true));
            }

            //AL: 15.04.2008 ==> BUG-FIX remove “()” if parametersString is empty
            if (parameters.Count > 0)
            {
                var result = DocsTypeService.GetXmlFromName(method.DeclaringType, 'M', method.Name + "({0})".With(String.Join(",", parameters)), false);

                // Try a fallback for case where a method with generic parameters is defined on base class.
                if (result == null)
                {
                    var typedParameters = new List<string>();

                    for (int i = 1; i <= parameters.Count; i++)
                    {
                        typedParameters.Add("`{0}".With(i));
                    }

                    return DocsTypeService.GetXmlFromName(method.DeclaringType, 'M', method.Name + "({0})".With(String.Join(",", typedParameters)), throwError);
                }

                if (result == null && throwError)
                {
                    ThrowHelper.ThrowDocNotFound();
                }

                return result;
            }
            else
            {
                return DocsTypeService.GetXmlFromName(method.DeclaringType, 'M', method.Name, throwError);
            }
        }
    }
}
