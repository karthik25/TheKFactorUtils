using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheKFactorUtils.Generators;

namespace TheKFactorUtils.Tests.Generators
{
    [TestClass]
    public class RandomStringGeneratorTests
    {
        [TestMethod]
        public void CanGenerateAStringWithThirtyTwoChars()
        {
            var str = RandomStringGenerator.RandomString();
            Assert.AreEqual(32, str.Length);
        }

        [TestMethod]
        public void CanGenerateAStringWithSizteenChars()
        {
            var str = RandomStringGenerator.RandomString(16);
            Assert.AreEqual(16, str.Length);
        }
    }
}
