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
                PathHelper.GetAssemblyDocFileNameFromLocation(null);
            });

            Assert.Throws<ArgumentNullException>(() =>
            {
                PathHelper.GetAssemblyDocFileNameFromLocation("");
            });
        }

        [Test()]
        public void GetAssemblyDocFileNameFromCodeBase_RightCodeBase_DocFileName()
        {
            var actual = PathHelper.GetAssemblyDocFileNameFromLocation("c:/Users/giacomelli/Dropbox/jogosdaqui/Plataforma/src/jogosdaqui.WebApi/Bin/jogosdaqui.WebApi.dll");

            Assert.AreEqual(
                "c:/Users/giacomelli/Dropbox/jogosdaqui/Plataforma/src/jogosdaqui.WebApi/Bin/jogosdaqui.WebApi.xml",
                actual
            );
        }
    }
}

