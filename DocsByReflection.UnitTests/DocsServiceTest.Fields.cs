using System.Reflection;
using DocsByReflection.UnitTests.Stubs;
using NUnit.Framework;

namespace DocsByReflection.UnitTests
{
    public partial class DocsServiceTest
    {
        [Test]
        public void GetXmlFromMember_FieldWithDoc_XmlElement()
        {
            var fieldInfo = typeof(Stub).GetField("FieldWithDoc", BindingFlags.Instance | BindingFlags.NonPublic);
            var actual = DocsService.GetXmlFromMember(fieldInfo);
            Assert.AreEqual("Gets or sets FieldWithDoc.", actual.SelectSingleNode("summary").InnerText.Trim());
        }

        [Test]
        public void GetXmlFromMember_FieldWithoutDoc_Null()
        {
            Assert.Throws<DocsByReflectionException>(() =>
            {
                var fieldInfo = typeof(Stub).GetField("FieldWithoutDoc", BindingFlags.Instance | BindingFlags.NonPublic);
                DocsService.GetXmlFromMember(fieldInfo);
            });
        }

        [Test]
        public void GetXmlFromMember_FieldWithoutDocNotThrow_Null()
        {
            var fieldInfo = typeof(Stub).GetField("FieldWithoutDoc", BindingFlags.Instance | BindingFlags.NonPublic);
            var actual = DocsService.GetXmlFromMember(fieldInfo, false);
            Assert.IsNull(actual);
        }

        [Test]
        public void GetXmlFromMember_FieldOnBaseClassWithDoc_XmlElement()
        {
            var fieldInfo = typeof(Stub).GetField("FieldOnBaseClassWithDoc", BindingFlags.Instance | BindingFlags.NonPublic);
            var actual = DocsService.GetXmlFromMember(fieldInfo);
            Assert.AreEqual("Gets or sets FieldOnBaseClassWithDoc.", actual.SelectSingleNode("summary").InnerText.Trim());
        }

        [Test]
        public void GetXmlFromMember_FieldGenericOnBaseClassWithDoc_XmlElement()
        {
            var fieldInfo = typeof(Stub).GetField("FieldGenericOnBaseClassWithDoc", BindingFlags.Instance | BindingFlags.NonPublic);
            var actual = DocsService.GetXmlFromMember(fieldInfo);
            Assert.AreEqual("Gets or sets FieldGenericOnBaseClassWithDoc.", actual.SelectSingleNode("summary").InnerText.Trim());
        }
    }
}
