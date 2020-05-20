using Microsoft.VisualStudio.TestTools.UnitTesting;
using PresentationBase.Converters;
using System.Windows;

namespace PresentationBase.Tests.Converters
{
    [TestClass]
    public class AllBoolToVisibilityConverterTests
        : ConverterTestsBase<AllBoolToVisibilityConverter>
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
            Assert.AreEqual(Visibility.Collapsed, result2);

            var result3 = _converter!.Convert(null, null, null, null);
            Assert.IsNotNull(result3);
            Assert.AreEqual(DependencyProperty.UnsetValue, result3);
        }
    }
}
