using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace PresentationBase.Converters
{
    /// <summary>
    /// Returns <see cref="Visibility.Visible"/> when converting <c>true</c>. Otherwise <see cref="Visibility.Collapsed"/>.
    /// </summary>
    [ValueConversion(typeof(bool), typeof(Visibility))]
    [MarkupExtensionReturnType(typeof(BoolToVisibilityConverter))]
    public class BoolToVisibilityConverter
        : ConverterBase
    {
        /// <inheritdoc/>
        public override object? Convert(object? value, Type? targetType, object? parameter, CultureInfo? culture)
        {
            if (value is not bool)
                return DependencyProperty.UnsetValue;

            return (bool)value ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <inheritdoc/>
        public override object? ConvertBack(object? value, Type? targetType, object? parameter, CultureInfo? culture)
        {
            if (value is not Visibility)
                return DependencyProperty.UnsetValue;

            return (Visibility)value == Visibility.Visible;
        }
    }
}