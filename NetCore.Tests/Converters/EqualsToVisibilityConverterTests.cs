using Microsoft.VisualStudio.TestTools.UnitTesting;
using PresentationBase.Converters;
using System.Windows;

namespace PresentationBase.Tests.Converters
{
    [TestClass]
    public class EqualsToVisibilityConverterTests
        : ConverterTestsBase<EqualsToVisibilityConverter>
    {
        [TestMethod]
        public override void Convert()
        {
            var result1 = _converter!.Convert(null, null, null, null);
            Assert.IsNotNull(result1);
            Assert.IsInstanceOfType(result1, typeof(Visibility));
            Assert.AreEqual(Visibility.Visible, result1);

            var result2 = _converter!.Convert(null, null, new object(), null);
            Assert.IsNotNull(result2);
            Assert.IsInstanceOfType(result2, typeof(Visibility));
            Assert.AreEqual(Visibility.Collapsed, result2);

            var result3 = _converter!.Convert("Something", null, "Something", null);
            Assert.IsNotNull(result3);
            Assert.IsInstanceOfType(result3, typeof(Visibility));
            Assert.AreEqual(Visibility.Visible, result3);

            var result4 = _converter!.Convert("Something", null, "Else", null);
            Assert.IsNotNull(result4);
            Assert.IsInstanceOfType(result4, typeof(Visibility));
            Assert.AreEqual(Visibility.Collapsed, result4);

            var result5 = _converter!.Convert("Something", null, 42, null);
            Assert.IsNotNull(result5);
            Assert.IsInstanceOfType(result5, typeof(Visibility));
            Assert.AreEqual(Visibility.Collapsed, result5);

            var result6 = _converter!.Convert("Something", null, null, null);
            Assert.IsNotNull(result6);
            Assert.IsInstanceOfType(result6, typeof(Visibility));
            Assert.AreEqual(Visibility.Collapsed, result6);

            var result7 = _converter!.Convert(3.14f, null, 3.14f, null);
            Assert.IsNotNull(result7);
            Assert.IsInstanceOfType(result7, typeof(Visibility));
            Assert.AreEqual(Visibility.Visible, result7);

            var result8 = _converter!.Convert(3.14f, null, 3.14d, null);
            Assert.IsNotNull(result8);
            Assert.IsInstanceOfType(result8, typeof(Visibility));
            Assert.AreEqual(Visibility.Collapsed, result8);

            var result9 = _converter!.Convert(3.14f, null, 1.337f, null);
            Assert.IsNotNull(result9);
            Assert.IsInstanceOfType(result9, typeof(Visibility));
            Assert.AreEqual(Visibility.Collapsed, result9);
        }
    }
}
