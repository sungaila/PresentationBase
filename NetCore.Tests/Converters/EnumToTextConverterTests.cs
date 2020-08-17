using Microsoft.VisualStudio.TestTools.UnitTesting;
using PresentationBase.Converters;
using System.Windows;

namespace PresentationBase.Tests.Converters
{
    [TestClass]
    public class EnumToTextConverterTests
        : ConverterTestsBase<EnumToTextConverter>
    {
        [TestMethod]
        public override void Convert()
        {
            var result1 = _converter!.Convert(TestEnum1.One, null, null, null);
            Assert.IsNotNull(result1);
            Assert.IsInstanceOfType(result1, typeof(string));
            Assert.AreEqual(nameof(TestEnum1.One), result1);

            var result2 = _converter!.Convert(TestEnum1.Two, null, null, null);
            Assert.IsNotNull(result2);
            Assert.IsInstanceOfType(result2, typeof(string));
            Assert.AreEqual(nameof(TestEnum1.Two), result2);

            var result3 = _converter!.Convert(TestEnum2.Dos, null, null, null);
            Assert.IsNotNull(result3);
            Assert.IsInstanceOfType(result3, typeof(string));
            Assert.AreEqual(nameof(TestEnum2.Dos), result3);

            var result4 = _converter!.Convert(TestEnum2.Tres, null, null, null);
            Assert.IsNotNull(result4);
            Assert.IsInstanceOfType(result4, typeof(string));
            Assert.AreEqual(nameof(TestEnum2.Tres), result4);

            var result5 = _converter!.Convert(null, null, null, null);
            Assert.IsNotNull(result5);
            Assert.AreEqual(DependencyProperty.UnsetValue, result5);

            var result6 = _converter!.Convert("Trash", null, null, null);
            Assert.IsNotNull(result6);
            Assert.AreEqual(DependencyProperty.UnsetValue, result6);
        }

        private enum TestEnum1
        {
            One,
            Two,
            Three
        }

        private enum TestEnum2 : long
        {
            Uno = 1,
            Dos = 2,
            Tres = 3
        }
    }
}
