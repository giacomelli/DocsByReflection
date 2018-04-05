using System;
using NUnit.Framework;

namespace DocsByReflection.UnitTests
{
    [TestFixture()]
    public class PathHelperTest
    {
        [Test()]
        public void GetAssemblyDocFileNameFromCodeBase_NullOrEmpty_Exception()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                PathHelper.GetAssemblyDocFileNameFromCodeBase(null);
            });

            Assert.Throws<ArgumentNullException>(() =>
            {
                PathHelper.GetAssemblyDocFileNameFromCodeBase("");
            });
        }

        [Test()]
        public void GetAssemblyDocFileNameFromCodeBase_DoesNotStartWithPrefix_Exception()
        {
            Assert.Throws<DocsByReflectionException>(() =>
            {
                PathHelper.GetAssemblyDocFileNameFromCodeBase("file://notExists");
            }, "Could not ascertain assembly filename from assembly code base 'file://notExists'.");
        }

        [Test()]
        public void GetAssemblyDocFileNameFromCodeBase_RightCodeBase_DocFileName()
        {
            var actual = PathHelper.GetAssemblyDocFileNameFromCodeBase("file:///Users/giacomelli/Dropbox/jogosdaqui/Plataforma/src/jogosdaqui.WebApi/Bin/jogosdaqui.WebApi.dll");

            if (Environment.OSVersion.Platform == PlatformID.MacOSX || Environment.OSVersion.Platform == PlatformID.Unix)
            {
                Assert.AreEqual("/Users/giacomelli/Dropbox/jogosdaqui/Plataforma/src/jogosdaqui.WebApi/Bin/jogosdaqui.WebApi.xml", actual);
            }
            else
            {
                Assert.AreEqual("Users/giacomelli/Dropbox/jogosdaqui/Plataforma/src/jogosdaqui.WebApi/Bin/jogosdaqui.WebApi.xml", actual);
            }
        }
    }
}

