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
        : MarkupExtension, IMultiValueConverter
    {
        /// <summary>
        /// A static instance of this value converter.
        /// </summary>
        public static readonly AllBoolToVisibilityReversedConverter Instance = new AllBoolToVisibilityReversedConverter();

        /// <inheritdoc/>
        public object? Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Any(v => !(v is bool)))
                return DependencyProperty.UnsetValue;

            return values.Cast<bool>().All(b => b) ? Visibility.Collapsed : Visibility.Visible;
        }

        /// <inheritdoc/>
        public object[]? ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Instance;
        }
    }
}
