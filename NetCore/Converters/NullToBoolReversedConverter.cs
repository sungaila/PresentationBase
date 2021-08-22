using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace PresentationBase.Converters
{
    /// <summary>
    /// Compares a given converter value to <see langword="null"/> and returns the <strong>negated</strong> result.
    /// </summary>
    [ValueConversion(typeof(object), typeof(bool))]
    [MarkupExtensionReturnType(typeof(NullToBoolReversedConverter))]
    public class NullToBoolReversedConverter
        : ConverterBase
    {
        /// <inheritdoc/>
        public override object? Convert(object? value, Type? targetType, object? parameter, CultureInfo? culture)
        {
            return value != null;
        }
    }
}
