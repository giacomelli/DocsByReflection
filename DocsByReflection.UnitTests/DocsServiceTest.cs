using DocsByReflection.UnitTests.Stubs;
using NUnit.Framework;

namespace DocsByReflection.UnitTests
{
    [TestFixture]
    public partial class DocsServiceTest
    {
        [Test]
        public void GetXmlFromType_Class_XmlElement()
        {
            var actual = DocsService.GetXmlFromType(typeof(Stub));
            Assert.AreEqual("Stub class.", actual.SelectSingleNode("summary").InnerText.Trim());
        }

        [Test]
        public void GetXmlFromAssembly_Assembly_XmlElement()
        {
            var actual = DocsService.GetXmlFromAssembly(typeof(Stub).Assembly);
            StringAssert.EndsWith("DocsByReflection.UnitTests.Stubs", actual.SelectSingleNode("//name").InnerText);
        }

        [Test]
        public void GetXmlFromType_GenericInterface_XmlElement()
        {
            var actual = DocsService.GetXmlFromType(typeof(GenericInterface<>));
            Assert.AreEqual("Summary of GenericInterface", actual.SelectSingleNode("summary").InnerText.Trim());
        }
    }
}
