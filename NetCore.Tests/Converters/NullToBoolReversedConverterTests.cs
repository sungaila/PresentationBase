using Microsoft.VisualStudio.TestTools.UnitTesting;
using PresentationBase.Converters;
using System.Windows;

namespace PresentationBase.Tests.Converters
{
    [TestClass]
    public class NullToBoolReversedConverterTests
        : ConverterTestsBase<NullToBoolReversedConverter>
    {
        [TestMethod]
        public override void Convert()
        {
            var result1 = _converter!.Convert("JC Denton", null, null, null);
            Assert.IsNotNull(result1);
            Assert.IsInstanceOfType(result1, typeof(bool));
            Assert.AreEqual(true, result1);

            var result2 = _converter!.Convert(null, null, null, null);
            Assert.IsNotNull(result2);
            Assert.IsInstanceOfType(result2, typeof(bool));
            Assert.AreEqual(false, result2);

            var result3 = _converter!.Convert(42, null, null, null);
            Assert.IsNotNull(result3);
            Assert.IsInstanceOfType(result3, typeof(bool));
            Assert.AreEqual(true, result3);

            var result4 = _converter!.Convert(true, null, null, null);
            Assert.IsNotNull(result4);
            Assert.IsInstanceOfType(result4, typeof(bool));
            Assert.AreEqual(true, result4);
        }
    }
}
