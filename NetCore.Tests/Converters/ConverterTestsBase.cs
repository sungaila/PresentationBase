﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using PresentationBase.Converters;
using System;
using System.Reflection;
using System.Windows.Data;
using System.Windows.Markup;

namespace PresentationBase.Tests.Converters
{
    public abstract class ConverterTestsBase<TConverter>
        where TConverter : class, IConverter, new()
    {
        protected TConverter? _converter = null;

        [TestInitialize]
        public void Initialize()
        {
            _converter = new TConverter();
        }

        [TestMethod]
        public void ProvideValue()
        {
            var result = _converter!.ProvideValue(null!);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(TConverter));
        }

        [TestMethod]
        public void Attributes()
        {
            var valueConversionAttr = _converter!.GetType().GetCustomAttribute<ValueConversionAttribute>(false);
            Assert.IsNotNull(valueConversionAttr);

            var returnTypeAttr = _converter!.GetType().GetCustomAttribute<MarkupExtensionReturnTypeAttribute>(false);
            Assert.IsNotNull(returnTypeAttr);
            Assert.AreEqual(typeof(TConverter), returnTypeAttr!.ReturnType);
        }

        public abstract void Convert();

        [TestMethod]
        public virtual void ConvertBack()
        {
            if (_converter is ConverterBase converter)
                Assert.ThrowsException<NotImplementedException>(() => converter.ConvertBack(null, null, null, null));
            else if (_converter is MultiConverterBase multiConverter)
                Assert.ThrowsException<NotImplementedException>(() => multiConverter.ConvertBack(null, null, null, null));
            else
                Assert.Fail($"Unknown converter type {_converter!.GetType()}");
        }
    }
}
