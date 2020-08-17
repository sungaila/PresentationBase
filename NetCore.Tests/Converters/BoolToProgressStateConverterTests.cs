using Microsoft.VisualStudio.TestTools.UnitTesting;
using PresentationBase.Converters;
using System.Windows;
using System.Windows.Shell;

namespace PresentationBase.Tests.Converters
{
    [TestClass]
    public class BoolToProgressStateConverterTests
        : ConverterTestsBase<BoolToProgressStateConverter>
    {
        [TestMethod]
        public override void Convert()
        {
            var result1 = _converter!.Convert(true, null, null, null);
            Assert.IsNotNull(result1);
            Assert.IsInstanceOfType(result1, typeof(TaskbarItemProgressState));
            Assert.AreEqual(TaskbarItemProgressState.Indeterminate, result1);

            var result2 = _converter!.Convert(false, null, null, null);
            Assert.IsNotNull(result2);
            Assert.IsInstanceOfType(result2, typeof(TaskbarItemProgressState));
            Assert.AreEqual(TaskbarItemProgressState.None, result2);

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
            var result1 = _converter!.ConvertBack(TaskbarItemProgressState.Indeterminate, null, null, null);
            Assert.IsNotNull(result1);
            Assert.IsInstanceOfType(result1, typeof(bool));
            Assert.AreEqual(true, result1);

            var result2 = _converter!.ConvertBack(TaskbarItemProgressState.None, null, null, null);
            Assert.IsNotNull(result2);
            Assert.IsInstanceOfType(result2, typeof(bool));
            Assert.AreEqual(false, result2);

            var result3 = _converter!.ConvertBack(null, null, null, null);
            Assert.IsNotNull(result3);
            Assert.AreEqual(DependencyProperty.UnsetValue, result3);

            var result4 = _converter!.ConvertBack("Trash", null, null, null);
            Assert.IsNotNull(result4);
            Assert.AreEqual(DependencyProperty.UnsetValue, result4);
        }
    }
}
