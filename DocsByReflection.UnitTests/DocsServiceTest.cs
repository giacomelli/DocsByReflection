using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using DocsByReflection.UnitTests.Stubs;

namespace DocsByReflection.UnitTests
{
	[TestClass]
	public class DocsServiceTest
	{
		#region GetXmlFromMember for properties
		[TestMethod]
		public void GetXmlFromMember_PropertyWithDoc_XmlElement()
		{
			var propertyInfo = typeof(Stub).GetProperty("PropertyWithDoc");
			var actual = DocsService.GetXmlFromMember(propertyInfo);
			Assert.AreEqual("Gets or sets PropertyWithDoc.", actual.SelectSingleNode("summary").InnerText.Trim());
		}

		[TestMethod]
		[ExpectedException(typeof(DocsByReflectionException))]
		public void GetXmlFromMember_PropertyWithoutDoc_Null()
		{
			var propertyInfo = typeof(Stub).GetProperty("PropertyWithoutDoc");
			var actual = DocsService.GetXmlFromMember(propertyInfo);
			Assert.IsNull(actual);
		}

		[TestMethod]
		public void GetXmlFromMember_PropertyOnBaseClassWithDoc_XmlElement()
		{
			var propertyInfo = typeof(Stub).GetProperty("PropertyOnBaseClassWithDoc");
			var actual = DocsService.GetXmlFromMember(propertyInfo);
			Assert.AreEqual("Gets or sets PropertyOnBaseClassWithDoc.", actual.SelectSingleNode("summary").InnerText.Trim());
		}

		[TestMethod]
		public void GetXmlFromMember_PropertyGenericOnBaseClassWithDoc_XmlElement()
		{
			var propertyInfo = typeof(Stub).GetProperty("PropertyGenericOnBaseClassWithDoc");
			var actual = DocsService.GetXmlFromMember(propertyInfo);
			Assert.AreEqual("Gets or sets PropertyGenericOnBaseClassWithDoc.", actual.SelectSingleNode("summary").InnerText.Trim());
		}
		#endregion

		#region GetXmlFromMember for methods
		[TestMethod]
		public void GetXmlFromMember_MethodWithGenericParameter_XmlElement()
		{
			var method = typeof(Stub).GetMethod("MethodWithGenericParameter");
			var actual = DocsService.GetXmlFromMember(method);
			Assert.AreEqual("MethodWithGenericParameter method.", actual.SelectSingleNode("summary").InnerText.Trim());
			Assert.AreEqual("Generic parameter.", actual.SelectSingleNode("param").InnerText.Trim());
		}
		#endregion

		#region GetXmlFromType
		[TestMethod]
		public void GetXmlFromType_Class_XmlElement()
		{
			var actual = DocsService.GetXmlFromType(typeof(Stub));
			Assert.AreEqual("Stub class.", actual.SelectSingleNode("summary").InnerText.Trim());			
		}
		#endregion

		#region GetXmlFromAssembly
		[TestMethod]
		public void GetXmlFromAssembly_Assembly_XmlElement()
		{
			var actual = DocsService.GetXmlFromAssembly(typeof(Stub).Assembly);
			Assert.AreEqual("DocsByReflection.UnitTests", actual.SelectSingleNode("//name").InnerText);
		}
		#endregion

		#region GetXmlFromParameter
		[TestMethod]
		public void GetXmlFromParameter_Parameter_XmlElement()
		{
			var method = typeof(Stub).GetMethod("MethodWithGenericParameter");
			var parameter = method.GetParameters()[0];
			var actual = DocsService.GetXmlFromParameter(parameter);
			Assert.AreEqual("Generic parameter.", actual.InnerText);
		}
		#endregion
	}
}
