using Microsoft.VisualStudio.TestTools.UnitTesting;
using PresentationBase.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PresentationBase.Tests.Extensions
{
    [TestClass]
    public class NaturalSortExtensionsTests
    {
        [TestMethod]
        public void Mixed()
        {
            var input = new string[] { "One", "Two", "Three", "One1", "Two2", "Three3" };
            var expected = new string[] { "One", "One1", "Three", "Three3", "Two", "Two2" };

            ValidateResult(expected, input.OrderByNatural(s => s, StringComparer.InvariantCulture));
            ValidateResult(expected.Reverse(), input.OrderByNaturalDescending(s => s, StringComparer.InvariantCulture));
        }

        [TestMethod]
        public void MultipleDigits()
        {
            var input = new string[] { "Something10Else", "Something11Else", "Something1Else", "Something2Else", "Something3Else", "Something0Else", "Something4Else", "Something5Else", "Something6Else", "Something7Else", "Something8Else", "Something9Else", "Something12Else" };
            var expected = new string[] { "Something0Else", "Something1Else", "Something2Else", "Something3Else", "Something4Else", "Something5Else", "Something6Else", "Something7Else", "Something8Else", "Something9Else", "Something10Else", "Something11Else", "Something12Else" };

            ValidateResult(expected, input.OrderByNatural(s => s, StringComparer.InvariantCulture));
            ValidateResult(expected.Reverse(), input.OrderByNaturalDescending(s => s, StringComparer.InvariantCulture));
        }

        [TestMethod]
        public void Prefix()
        {
            var input = new string[] { "12Stuff", "1Stuff", "3Stuff", "2Stuff", "0002Stuff", "01Stuff", "11Stuff", "0011Stuff", "10Stuff" };
            var expected = new string[] { "1Stuff", "01Stuff", "2Stuff", "0002Stuff", "3Stuff", "10Stuff", "11Stuff", "0011Stuff", "12Stuff" };

            ValidateResult(expected, input.OrderByNatural(s => s, StringComparer.InvariantCulture));
            ValidateResult(expected.Reverse(), input.OrderByNaturalDescending(s => s, StringComparer.InvariantCulture));
        }

        private static void ValidateResult(IEnumerable<string> expected, IEnumerable<string> actual)
        {
            Assert.IsNotNull(expected);
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Count(), actual.Count());

            for (int i = 0; i < actual.Count(); i++)
            {
                Assert.AreEqual(expected.ElementAt(i), actual.ElementAt(i));
            }
        }
    }
}
