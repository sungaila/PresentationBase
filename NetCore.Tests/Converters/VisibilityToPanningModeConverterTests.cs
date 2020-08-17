using Microsoft.VisualStudio.TestTools.UnitTesting;
using PresentationBase.Converters;
using System.Windows;
using System.Windows.Controls;

namespace PresentationBase.Tests.Converters
{
    [TestClass]
    public class VisibilityToPanningModeConverterTests
        : ConverterTestsBase<VisibilityToPanningModeConverter>
    {
        [TestMethod]
        public override void Convert()
        {
            var result1 = _converter!.Convert(null, null, null, null);
            Assert.IsNotNull(result1);
            Assert.AreEqual(DependencyProperty.UnsetValue, result1);

            var result2 = _converter!.Convert("Trash", null, null, null);
            Assert.IsNotNull(result2);
            Assert.AreEqual(DependencyProperty.UnsetValue, result2);

            var result3 = _converter!.Convert(null, null, "Trash", null);
            Assert.IsNotNull(result3);
            Assert.AreEqual(DependencyProperty.UnsetValue, result3);

            var result4 = _converter!.Convert(Visibility.Visible, null, PanningMode.Both, null);
            Assert.IsNotNull(result4);
            Assert.IsInstanceOfType(result4, typeof(PanningMode));
            Assert.AreEqual(PanningMode.Both, result4);

            var result5 = _converter!.Convert(Visibility.Collapsed, null, PanningMode.Both, null);
            Assert.IsNotNull(result5);
            Assert.IsInstanceOfType(result5, typeof(PanningMode));
            Assert.AreEqual(PanningMode.None, result5);

            var result6 = _converter!.Convert(Visibility.Visible, null, PanningMode.VerticalFirst, null);
            Assert.IsNotNull(result6);
            Assert.IsInstanceOfType(result6, typeof(PanningMode));
            Assert.AreEqual(PanningMode.VerticalFirst, result6);

            var result7 = _converter!.Convert(Visibility.Hidden, null, PanningMode.VerticalFirst, null);
            Assert.IsNotNull(result7);
            Assert.IsInstanceOfType(result7, typeof(PanningMode));
            Assert.AreEqual(PanningMode.None, result7);
        }
    }
}
