using System;
using NUnit.Framework;
using TestSharp;

namespace DocsByReflection.UnitTests
{
    [TestFixture()]
    public class PathHelperTest
    {
        [Test()]
        public void GetAssemblyDocFileNameFromCodeBase_NullOrEmpty_Exception()
        {
            ExceptionAssert.IsThrowing(new ArgumentNullException("assemblyCodeBase"), () =>
            {
                PathHelper.GetAssemblyDocFileNameFromCodeBase(null);
            });

            ExceptionAssert.IsThrowing(new ArgumentNullException("assemblyCodeBase"), () =>
            {
                PathHelper.GetAssemblyDocFileNameFromCodeBase("");
            });
        }

        [Test()]
        public void GetAssemblyDocFileNameFromCodeBase_DoesNotStartWithPrefix_Exception()
        {
            ExceptionAssert.IsThrowing(new DocsByReflectionException("Could not ascertain assembly filename from assembly code base 'file://notExists'."), () =>
            {
                PathHelper.GetAssemblyDocFileNameFromCodeBase("file://notExists");
            });
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

