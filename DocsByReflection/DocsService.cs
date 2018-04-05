//Except where stated all code and programs in this project are the copyright of Jim Blackler, 2008.
//jimblackler@gmail.com
//
//This is free software. Libraries and programs are distributed under the terms of the GNU Lesser
//General Public License. Please see the files COPYING and COPYING.LESSER.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using System.Xml;

namespace DocsByReflection
{
    /// <summary>
    /// Utility class to provide documentation for various types where available with the assembly
    /// </summary>
    public static class DocsService
    {
        #region Public methods
        /// <summary>
        /// Provides the documentation comments for a specific method
        /// </summary>
        /// <param name="method">The MethodInfo (reflection data ) of the member to find documentation for</param>
        /// <param name="throwError">If should throw error when documentation is not found. Default is true.</param>
        /// <returns>The XML fragment describing the method</returns>
        [SuppressMessage("Microsoft.Design", "CA1059:MembersShouldNotExposeCertainConcreteTypes", MessageId = "System.Xml.XmlNode")]
        public static XmlElement GetXmlFromMember(MethodBase method, bool throwError = true)
        {
            return DocsMethodService.GetXmlFromMember(method, throwError);
        }

        /// <summary>
        /// Provides the documentation comments for a specific member.
        /// </summary>
        /// <param name="member">The MemberInfo (reflection data) or the member to find documentation for</param>
        /// <param name="throwError">If should throw error when documentation is not found. Default is true.</param>
        /// <returns>The XML fragment describing the member</returns>
        [SuppressMessage("Microsoft.Design", "CA1059:MembersShouldNotExposeCertainConcreteTypes", MessageId = "System.Xml.XmlNode")]
        public static XmlElement GetXmlFromMember(MemberInfo member, bool throwError = true)
        {
            if (member == null)
                throw new ArgumentNullException(nameof(member));

            // First character [0] of member type is prefix character in the name in the XML
            var prefix = member.MemberType.ToString()[0];
            ParameterInfo[] parameters = null;
            if (((PropertyInfo)member).CanRead)
               parameters = ((PropertyInfo)member).GetGetMethod().GetParameters();
            var strParameters = new List<string>();
            //Handles getter/setter style properties
            if (parameters != null && parameters.Length > 0)
               {
                   foreach (ParameterInfo parameterInfo in parameters)
                    strParameters.Add(DocsTypeService.GetTypeFullNameForXmlDoc(parameterInfo.ParameterType, parameterInfo.IsOut | parameterInfo.ParameterType.IsByRef, true));
                   return DocsTypeService.GetXmlFromName(member.ReflectedType, prefix, string.Format(member.Name + "({0})", string.Join(",", strParameters)), throwError);
               }
            //handles regular properties
            else
                return DocsTypeService.GetXmlFromName(member.DeclaringType, member.MemberType.ToString()[0], member.Name, throwError);
        }

        /// <summary>
        /// Provides the documentation comments for a specific parameter.
        /// </summary>
        /// <param name="parameter">The ParameterInfo (reflection data) to find documentation for.</param>
        /// <param name="throwError">If should throw error when documentation is not found. Default is true.</param>
        /// <returns>The XML fragment describing the paramter.</returns>
        [SuppressMessage("Microsoft.Design", "CA1059:MembersShouldNotExposeCertainConcreteTypes", MessageId = "System.Xml.XmlNode")]
        public static XmlElement GetXmlFromParameter(ParameterInfo parameter, bool throwError = true)
        {
            if (parameter == null)
                throw new ArgumentNullException(nameof(parameter));

            var method = parameter.Member as MethodBase;
            var memberDoc = method == null ? GetXmlFromMember(parameter.Member, throwError) : GetXmlFromMember(method, throwError);

            if (memberDoc == null)
            {
                return null;
            }

            return memberDoc.SelectSingleNode(string.Format(CultureInfo.InvariantCulture, "param[@name='{0}']", parameter.Name)) as XmlElement;
        }

        /// <summary>
        /// Provides the documentation comments for a specific type
        /// </summary>
        /// <param name="type">Type to find the documentation for</param>
        /// <param name="throwError">If should throw error when documentation is not found. Default is true.</param>
        /// <returns>The XML fragment that describes the type</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1059:MembersShouldNotExposeCertainConcreteTypes", MessageId = "System.Xml.XmlNode")]
        public static XmlElement GetXmlFromType(Type type, bool throwError = true)
        {
            // Prefix in type names is T
            return DocsTypeService.GetXmlFromName(type, 'T', "", throwError);
        }

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
            return DocsAssemblyService.GetXmlFromAssembly(assembly, throwError);
        }
        #endregion
    }
}
