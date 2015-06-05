using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheKFactorUtils.RegexUtils;

namespace TheKFactorUtils.Tests.RegEx
{
    [TestClass]
    public class RegExUtilTests
    {
        [TestMethod]
        public void CanGetAnEnumerableForMatchedGroups()
        {
            var regex = new Regex(@"(\d+)\-(\d+)\-(\d+)");
            const string src = "987-654-3210";
            var matchGroups = src.GetMatchValues(regex);
            var expectedValues = new List<string> { "987", "654", "3210" };
            var index = 0;
            foreach (var matchGroup in matchGroups)
            {
                Assert.AreEqual(expectedValues[index], matchGroup);
                index++;
            }
        }
    }
}
