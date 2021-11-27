using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace PresentationBase.Converters
{
    /// <summary>
    /// Base class for multi converters in PresentationBase.
    /// </summary>
    public abstract class MultiConverterBase
        : MarkupExtension, IMultiValueConverter, IConverter
    {
        private MultiConverterBase? _instance;

        /// <inheritdoc/>
        private MultiConverterBase Instance
        {
            get
            {
                if (_instance == null)
                    _instance = (Activator.CreateInstance(GetType()) as MultiConverterBase)!;

                return _instance;
            }
        }

        /// <inheritdoc/>
        IConverter IConverter.Instance => Instance;

        /// <inheritdoc/>
        public sealed override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Instance;
        }

        /// <inheritdoc/>
        public abstract object? Convert(object[]? values, Type? targetType, object? parameter, CultureInfo? culture);

        /// <inheritdoc/>
        public virtual object[]? ConvertBack(object? value, Type[]? targetTypes, object? parameter, CultureInfo? culture)
        {
            throw new NotImplementedException();
        }
    }
}