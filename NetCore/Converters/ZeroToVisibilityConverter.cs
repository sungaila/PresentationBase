using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace PresentationBase.Converters
{
    /// <summary>
    /// Returns <see cref="Visibility.Visible"/> when the given converter value is a <c>0</c> (<see cref="int"/>).
    /// Otherwise <see cref="Visibility.Collapsed"/>.
    /// </summary>
    [ValueConversion(typeof(int), typeof(Visibility))]
    [MarkupExtensionReturnType(typeof(ZeroToVisibilityConverter))]
    public class ZeroToVisibilityConverter
        : ConverterBase
    {
        /// <inheritdoc/>
        public override object? Convert(object? value, Type? targetType, object? parameter, CultureInfo? culture)
        {
            if (value is not int)
                return Visibility.Collapsed;

            return (int)value == 0 ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}