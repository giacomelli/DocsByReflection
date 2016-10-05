using DocsByReflection.UnitTests.Stubs;
using NUnit.Framework;
using TestSharp;

namespace DocsByReflection.UnitTests
{
    [TestFixture]
    public class DocsServiceTest
    {
        #region GetXmlFromMember for properties
        [Test]
        public void GetXmlFromMember_PropertyWithDoc_XmlElement()
        {
            var propertyInfo = typeof(Stub).GetProperty("PropertyWithDoc");
            var actual = DocsService.GetXmlFromMember(propertyInfo);
            Assert.AreEqual("Gets or sets PropertyWithDoc.", actual.SelectSingleNode("summary").InnerText.Trim());
        }

        [Test]
        [ExpectedException(typeof(DocsByReflectionException))]
        public void GetXmlFromMember_PropertyWithoutDoc_Null()
        {
            var propertyInfo = typeof(Stub).GetProperty("PropertyWithoutDoc");
            DocsService.GetXmlFromMember(propertyInfo);
        }

        [Test]
        public void GetXmlFromMember_PropertyWithoutDocNotThrow_Null()
        {
            var propertyInfo = typeof(Stub).GetProperty("PropertyWithoutDoc");
            var actual = DocsService.GetXmlFromMember(propertyInfo, false);
            Assert.IsNull(actual);
        }

        [Test]
        public void GetXmlFromMember_PropertyOnBaseClassWithDoc_XmlElement()
        {
            var propertyInfo = typeof(Stub).GetProperty("PropertyOnBaseClassWithDoc");
            var actual = DocsService.GetXmlFromMember(propertyInfo);
            Assert.AreEqual("Gets or sets PropertyOnBaseClassWithDoc.", actual.SelectSingleNode("summary").InnerText.Trim());
        }

        [Test]
        public void GetXmlFromMember_PropertyGenericOnBaseClassWithDoc_XmlElement()
        {
            var propertyInfo = typeof(Stub).GetProperty("PropertyGenericOnBaseClassWithDoc");
            var actual = DocsService.GetXmlFromMember(propertyInfo);
            Assert.AreEqual("Gets or sets PropertyGenericOnBaseClassWithDoc.", actual.SelectSingleNode("summary").InnerText.Trim());
        }
        #endregion

        #region GetXmlFromMember for methods
        [Test]
        public void GetXmlFromMember_MethodWithGenericParameter_XmlElement()
        {
            var method = typeof(Stub).GetMethod("MethodWithGenericParameter");
            var actual = DocsService.GetXmlFromMember(method);
            Assert.AreEqual("MethodWithGenericParameter method.", actual.SelectSingleNode("summary").InnerText.Trim());
            Assert.AreEqual("Generic parameter.", actual.SelectSingleNode("param").InnerText.Trim());
        }
        [Test]
        public void GetXmlFromMember_MethodWithComplexGenericParameter_XmlElement()
        {
            var method = typeof(Stub).GetMethod("MethodWithComplexGenericParameter");
            var actual = DocsService.GetXmlFromMember(method);
            Assert.AreEqual("MethodWithComplexGenericParameter method.", actual.SelectSingleNode("summary").InnerText.Trim());
            Assert.AreEqual("Generic dictionary parameter.", actual.SelectSingleNode("param").InnerText.Trim());
        }

        [Test]
        public void GetXmlFromMember_MethodWithGenericArgumentOnBaseClass_XmlElement()
        {
            var method = typeof(Stub).GetMethod("Create");
            var actual = DocsService.GetXmlFromMember(method);
            Assert.AreEqual("Creates the specified entity.", actual.SelectSingleNode("summary").InnerText.Trim());
            Assert.AreEqual("The entity.", actual.SelectSingleNode("param").InnerText.Trim());
        }
        #endregion

        #region GetXmlFromType
        [Test]
        public void GetXmlFromType_Class_XmlElement()
        {
            var actual = DocsService.GetXmlFromType(typeof(Stub));
            Assert.AreEqual("Stub class.", actual.SelectSingleNode("summary").InnerText.Trim());
        }
        #endregion

        #region GetXmlFromAssembly
        [Test]
        public void GetXmlFromAssembly_Assembly_XmlElement()
        {
            var actual = DocsService.GetXmlFromAssembly(typeof(Stub).Assembly);
            StringAssert.EndsWith("DocsByReflection.UnitTests.Stubs", actual.SelectSingleNode("//name").InnerText);
        }
        #endregion

        #region GetXmlFromParameter
        [Test]
        public void GetXmlFromParameter_Parameter_XmlElement()
        {
            var method = typeof(Stub).GetMethod("MethodWithGenericParameter");
            var parameter = method.GetParameters()[0];
            var actual = DocsService.GetXmlFromParameter(parameter);
            Assert.AreEqual("Generic parameter.", actual.InnerText);
        }

        [Test]
        public void GetXmlFromParameter_ParameterNoDoc_XmlElement()
        {
            var method = typeof(Stub).GetMethod("MethodWithoutDoc");
            var parameter = method.GetParameters()[0];

            ExceptionAssert.IsThrowing(new DocsByReflectionException("Could not find documentation for specified element", null), () =>
            {
                DocsService.GetXmlFromParameter(parameter);
            });
        }

        [Test]
        public void GetXmlFromParameter_ParameterNoDocNotThrow_XmlElement()
        {
            var method = typeof(Stub).GetMethod("MethodWithoutDoc");
            var parameter = method.GetParameters()[0];

            var actual = DocsService.GetXmlFromParameter(parameter, false);
            Assert.IsNull(actual);
        }
        #endregion
    }
}
