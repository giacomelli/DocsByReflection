using DocsByReflection.UnitTests.Stubs;
using NUnit.Framework;

namespace DocsByReflection.UnitTests
{
    [TestFixture]
    public partial class DocsServiceTest
    {
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

        [Test]
        public void GetXmlFromMember_MethodWithCollectionReturnType_XmlElement()
        {
            var method = typeof(Stub).GetMethod("MethodWithCollectionReturnType");
            var actual = DocsService.GetXmlFromMember(method);
            Assert.AreEqual("MethodWithCollectionReturnType method.", actual.SelectSingleNode("summary").InnerText.Trim());
        }
 
        [Test]
        public void GetXmlFromMember_MethodWithGenericType_XmlElement()
        {
            var method = typeof(Stub).GetMethod("MethodWithGenericType");
            var actual = DocsService.GetXmlFromMember(method);
            Assert.AreEqual("MethodWithGenericType method.", actual.SelectSingleNode("summary").InnerText.Trim());
        }
 
        [Test]
        public void GetXmlFromMember_MethodWithGenericCollectionType_XmlElement()
        {
            var method = typeof(Stub).GetMethod("MethodWithGenericCollectionType");
            var actual = DocsService.GetXmlFromMember(method);
            Assert.AreEqual("MethodWithGenericCollectionType method.", actual.SelectSingleNode("summary").InnerText.Trim());
        }
 
        [Test]
        public void GetXmlFromMember_MethodWithOutParameter_XmlElement()
        {
            var method = typeof(Stub).GetMethod("MethodWithOutParameter");
            var actual = DocsService.GetXmlFromMember(method);
            Assert.AreEqual("MethodWithOutParameter method.", actual.SelectSingleNode("summary").InnerText.Trim());
        }
 
        [Test]
        public void GetXmlFromMember_MethodWithCollectionOutParameter_XmlElement()
        {
            var method = typeof(Stub).GetMethod("MethodWithCollectionOutParameter");
            var actual = DocsService.GetXmlFromMember(method);
            Assert.AreEqual("MethodWithCollectionOutParameter method.", actual.SelectSingleNode("summary").InnerText.Trim());
        }
 
        [Test]
        public void GetXmlFromMember_MethodWithCollectionOutGenericTypeParameter_XmlElement()
        {
            var method = typeof(Stub).GetMethod("MethodWithCollectionOutGenericTypeParameter");
            var actual = DocsService.GetXmlFromMember(method);
            Assert.AreEqual("MethodWithCollectionOutGenericTypeParameter method.", actual.SelectSingleNode("summary").InnerText.Trim());
        }

        [Test]
        public void GetXmlFromMember_MethodWithCollectionOfInnerClass()
        {
            var method = typeof(Stub).GetMethod("MethodWithCollectionOfInnerClass");
            var actual = DocsService.GetXmlFromMember(method);
            Assert.AreEqual("MethodWithCollectionOfInnerClass method.", actual.SelectSingleNode("summary").InnerText.Trim());
        }
    }
}
