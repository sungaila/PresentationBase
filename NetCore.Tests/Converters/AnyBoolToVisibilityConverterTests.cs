using Microsoft.VisualStudio.TestTools.UnitTesting;
using PresentationBase.Converters;
using System.Windows;

namespace PresentationBase.Tests.Converters
{
    [TestClass]
    public class AnyBoolToVisibilityConverterTests
        : ConverterTestsBase<AnyBoolToVisibilityConverter>
    {
        [TestMethod]
        public override void Convert()
        {
            var result1 = _converter!.Convert(new object[] { true, true }, null, null, null);
            Assert.IsNotNull(result1);
            Assert.IsInstanceOfType(result1, typeof(Visibility));
            Assert.AreEqual(Visibility.Visible, result1);

            var result2 = _converter!.Convert(new object[] { true, false }, null, null, null);
            Assert.IsNotNull(result2);
            Assert.IsInstanceOfType(result2, typeof(Visibility));
            Assert.AreEqual(Visibility.Visible, result2);

            var result3 = _converter!.Convert(new object[] { false, false }, null, null, null);
            Assert.IsNotNull(result3);
            Assert.IsInstanceOfType(result3, typeof(Visibility));
            Assert.AreEqual(Visibility.Collapsed, result3);

            var result4 = _converter!.Convert(null, null, null, null);
            Assert.IsNotNull(result4);
            Assert.AreEqual(DependencyProperty.UnsetValue, result4);
        }
    }
}
