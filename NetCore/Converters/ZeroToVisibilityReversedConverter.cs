using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace PresentationBase.Converters
{
    /// <summary>
    /// Returns <see cref="Visibility.Visible"/> when the given converter value is <strong>not</strong> <c>0</c> (<see cref="int"/>).
    /// Otherwise <see cref="Visibility.Collapsed"/>.
    /// </summary>
    [ValueConversion(typeof(int), typeof(Visibility))]
    [MarkupExtensionReturnType(typeof(ZeroToVisibilityReversedConverter))]
    public class ZeroToVisibilityReversedConverter
        : ConverterBase
    {
        /// <inheritdoc/>
        public override object? Convert(object? value, Type? targetType, object? parameter, CultureInfo? culture)
        {
            if (!(value is int))
                return Visibility.Visible;

            return (int)value == 0 ? Visibility.Collapsed : Visibility.Visible;
        }
    }
}
