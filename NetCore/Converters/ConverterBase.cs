using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace PresentationBase.Converters
{
    /// <summary>
    /// Base class for converters in PresentationBase.
    /// </summary>
    public abstract class ConverterBase
        : MarkupExtension, IValueConverter, IConverter
    {
        private ConverterBase? _instance;

        /// <inheritdoc/>
        private ConverterBase Instance
        {
            get
            {
                if (_instance == null)
                    _instance = (Activator.CreateInstance(GetType()) as ConverterBase)!;

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
        public abstract object? Convert(object? value, Type? targetType, object? parameter, CultureInfo? culture);

        /// <inheritdoc/>
        public virtual object? ConvertBack(object? value, Type? targetType, object? parameter, CultureInfo? culture)
        {
            throw new NotImplementedException();
        }
    }
}
