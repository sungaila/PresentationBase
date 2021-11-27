using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace PresentationBase.Converters
{
    /// <summary>
    /// Converts multiple <see cref="bool"/>s into a <see cref="Visibility"/>.
    /// Returns <see cref="Visibility.Collapsed"/> when <strong>all</strong> bools are true. Otherwise <see cref="Visibility.Visible"/>.
    /// </summary>
    [ValueConversion(typeof(bool), typeof(Visibility))]
    [MarkupExtensionReturnType(typeof(AllBoolToVisibilityReversedConverter))]
    public class AllBoolToVisibilityReversedConverter
        : MultiConverterBase
    {
        /// <inheritdoc/>
        public override object? Convert(object[]? values, Type? targetType, object? parameter, CultureInfo? culture)
        {
            if (values == null || values.Any(v => v is not bool))
                return DependencyProperty.UnsetValue;

            return values.Cast<bool>().All(b => b) ? Visibility.Collapsed : Visibility.Visible;
        }
    }
}