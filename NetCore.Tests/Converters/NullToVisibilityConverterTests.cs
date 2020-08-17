using Microsoft.VisualStudio.TestTools.UnitTesting;
using PresentationBase.Converters;
using System.Windows;

namespace PresentationBase.Tests.Converters
{
    [TestClass]
    public class NullToVisibilityConverterTests
        : ConverterTestsBase<NullToVisibilityConverter>
    {
        [TestMethod]
        public override void Convert()
        {
            var result1 = _converter!.Convert("JC Denton", null, null, null);
            Assert.IsNotNull(result1);
            Assert.IsInstanceOfType(result1, typeof(Visibility));
            Assert.AreEqual(Visibility.Collapsed, result1);

            var result2 = _converter!.Convert(null, null, null, null);
            Assert.IsNotNull(result2);
            Assert.IsInstanceOfType(result2, typeof(Visibility));
            Assert.AreEqual(Visibility.Visible, result2);

            var result3 = _converter!.Convert(42, null, null, null);
            Assert.IsNotNull(result3);
            Assert.IsInstanceOfType(result3, typeof(Visibility));
            Assert.AreEqual(Visibility.Collapsed, result3);

            var result4 = _converter!.Convert(true, null, null, null);
            Assert.IsNotNull(result4);
            Assert.IsInstanceOfType(result4, typeof(Visibility));
            Assert.AreEqual(Visibility.Collapsed, result4);
        }
    }
}
