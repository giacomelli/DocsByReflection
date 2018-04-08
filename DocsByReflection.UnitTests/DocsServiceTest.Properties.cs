using DocsByReflection.UnitTests.Stubs;
using NUnit.Framework;

namespace DocsByReflection.UnitTests
{
    [TestFixture]
    public partial class DocsServiceTest
    {
        [Test]
        public void GetXmlFromMember_PropertyWithDoc_XmlElement()
        {
            var propertyInfo = typeof(Stub).GetProperty("PropertyWithDoc");
            var actual = DocsService.GetXmlFromMember(propertyInfo);
            Assert.AreEqual("Gets or sets PropertyWithDoc.", actual.SelectSingleNode("summary").InnerText.Trim());
        }

        [Test]
        public void GetXmlFromMember_PropertyWithoutDoc_Null()
        {
            Assert.Throws<DocsByReflectionException>(() =>
            {
                var propertyInfo = typeof(Stub).GetProperty("PropertyWithoutDoc");
                DocsService.GetXmlFromMember(propertyInfo);
            });
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
    }
}
