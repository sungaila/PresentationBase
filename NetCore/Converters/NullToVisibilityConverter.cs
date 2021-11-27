using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace PresentationBase.Converters
{
    /// <summary>
    /// Compares a given converter value to <see langword="null"/>.
    /// Returns <see cref="Visibility.Visible"/> when <c>true</c>. Otherwise <see cref="Visibility.Collapsed"/>.
    /// </summary>
    [ValueConversion(typeof(object), typeof(Visibility))]
    [MarkupExtensionReturnType(typeof(NullToVisibilityConverter))]
    public class NullToVisibilityConverter
        : ConverterBase
    {
        /// <inheritdoc/>
        public override object? Convert(object? value, Type? targetType, object? parameter, CultureInfo? culture)
        {
            return value == null ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}