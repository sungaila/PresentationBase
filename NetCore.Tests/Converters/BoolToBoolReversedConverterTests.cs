using Microsoft.VisualStudio.TestTools.UnitTesting;
using PresentationBase.Converters;
using System.Windows;

namespace PresentationBase.Tests.Converters
{
    [TestClass]
    public class BoolToBoolReversedConverterTests
        : ConverterTestsBase<BoolToBoolReversedConverter>
    {
        [TestMethod]
        public override void Convert()
        {
            var result1 = _converter!.Convert(true, null, null, null);
            Assert.IsNotNull(result1);
            Assert.IsInstanceOfType(result1, typeof(bool));
            Assert.AreEqual(false, result1);

            var result2 = _converter!.Convert(false, null, null, null);
            Assert.IsNotNull(result2);
            Assert.IsInstanceOfType(result2, typeof(bool));
            Assert.AreEqual(true, result2);

            var result3 = _converter!.Convert(null, null, null, null);
            Assert.IsNotNull(result3);
            Assert.AreEqual(DependencyProperty.UnsetValue, result3);

            var result4 = _converter!.Convert("Trash", null, null, null);
            Assert.IsNotNull(result4);
            Assert.AreEqual(DependencyProperty.UnsetValue, result4);
        }

        [TestMethod]
        public override void ConvertBack()
        {
            var result1 = _converter!.ConvertBack(true, null, null, null);
            Assert.IsNotNull(result1);
            Assert.IsInstanceOfType(result1, typeof(bool));
            Assert.AreEqual(false, result1);

            var result2 = _converter!.ConvertBack(false, null, null, null);
            Assert.IsNotNull(result2);
            Assert.IsInstanceOfType(result2, typeof(bool));
            Assert.AreEqual(true, result2);

            var result3 = _converter!.ConvertBack(null, null, null, null);
            Assert.IsNotNull(result3);
            Assert.AreEqual(DependencyProperty.UnsetValue, result3);

            var result4 = _converter!.ConvertBack("Trash", null, null, null);
            Assert.IsNotNull(result4);
            Assert.AreEqual(DependencyProperty.UnsetValue, result4);
        }
    }
}
