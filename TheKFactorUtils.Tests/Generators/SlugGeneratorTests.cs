using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheKFactorUtils.Generators;

namespace TheKFactorUtils.Tests.Generators
{
    [TestClass]
    public class SlugGeneratorTests
    {
        [TestMethod]
        public void CanGenerateASlug()
        {
            const string rawString = "a unique url";
            var currentUrls = new List<string>();
            var slug = rawString.GetUniqueSlug(currentUrls);
            Assert.AreEqual("a-unique-url", slug);
        }

        [TestMethod]
        public void CanGenerateASlugForAnExistingUrl()
        {
            const string rawString = "a unique url";
            var currentUrls = new List<string>{ "a-unique-url" };
            var slug = rawString.GetUniqueSlug(currentUrls);
            Assert.AreEqual("a-unique-url-2", slug);
        }

        [TestMethod]
        public void CanTakeCareOfLeadingHyphens()
        {
            const string rawString = " a unique url";
            var currentUrls = new List<string>();
            var slug = rawString.GetUniqueSlug(currentUrls);
            Assert.AreEqual("0-a-unique-url", slug);
        }

        [TestMethod]
        public void CanTakeCareOfTrailingHyphens()
        {
            const string rawString = "a unique url ";
            var currentUrls = new List<string>();
            var slug = rawString.GetUniqueSlug(currentUrls);
            Assert.AreEqual("a-unique-url-0", slug);
        }
    }
}
