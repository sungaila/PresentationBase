using Microsoft.VisualStudio.TestTools.UnitTesting;
using PresentationBase.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PresentationBase.Tests.Extensions
{
    [TestClass]
    public class NaturalSortHelperTests
    {
        [TestMethod]
        public void Padding()
        {
            var viewModel = new TestViewModel();

            var input = new string[] { viewModel.Helper[nameof(viewModel.Value3)]!, viewModel.Helper[nameof(viewModel.Value2)]!, viewModel.Helper[nameof(viewModel.Value1)]!, viewModel.Helper[nameof(viewModel.Value4)]! };
            var expected = new string[] { viewModel.Helper[nameof(viewModel.Value1)]!, viewModel.Helper[nameof(viewModel.Value2)]!, viewModel.Helper[nameof(viewModel.Value3)]!, viewModel.Helper[nameof(viewModel.Value4)]! };

            ValidateResult(expected.OrderBy(s => s, StringComparer.InvariantCulture), input.OrderBy(s => s, StringComparer.InvariantCulture));
            ValidateResult(expected.OrderByDescending(s => s, StringComparer.InvariantCulture), input.OrderByDescending(s => s, StringComparer.InvariantCulture));
        }

        private class TestViewModel : ViewModel
        {
            private string _value1 = "Something1";

            public string Value1
            {
                get => _value1;
                set => SetProperty(ref _value1, value);
            }

            private string _value2 = "Something2";

            public string Value2
            {
                get => _value2;
                set => SetProperty(ref _value3, value);
            }

            private string _value3 = "Something11";

            public string Value3
            {
                get => _value3;
                set => SetProperty(ref _value3, value);
            }

            private string _value4 = "Something12";

            public string Value4
            {
                get => _value4;
                set => SetProperty(ref _value4, value);
            }

            public NaturalSortHelper Helper { get; }

            public TestViewModel()
            {
                Helper = new NaturalSortHelper(this);
            }
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
