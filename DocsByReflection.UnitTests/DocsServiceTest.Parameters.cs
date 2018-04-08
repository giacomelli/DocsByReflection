using DocsByReflection.UnitTests.Stubs;
using NUnit.Framework;

namespace DocsByReflection.UnitTests
{
    [TestFixture]
    public partial class DocsServiceTest
    {
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

            Assert.Throws< DocsByReflectionException>(() =>
            {
                DocsService.GetXmlFromParameter(parameter);
            }, "Could not find documentation for specified element");
        }

        [Test]
        public void GetXmlFromParameter_ParameterNoDocNotThrow_XmlElement()
        {
            var method = typeof(Stub).GetMethod("MethodWithoutDoc");
            var parameter = method.GetParameters()[0];

            var actual = DocsService.GetXmlFromParameter(parameter, false);
            Assert.IsNull(actual);
        }
    }
}
