using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace PresentationBase.Converters
{
	/// <summary>
	/// Converts multiple <see cref="bool"/>s into a <see cref="Visibility"/>.
	/// Returns <see cref="Visibility.Visible"/> when <strong>any</strong> bool is true. Otherwise <see cref="Visibility.Collapsed"/>.
	/// </summary>
	[ValueConversion(typeof(bool), typeof(Visibility))]
	public class AnyBoolToVisibilityConverter
		: IMultiValueConverter
	{
		/// <summary>
		/// A static instance of this value converter.
		/// </summary>
		public static readonly AnyBoolToVisibilityConverter Instance = new AnyBoolToVisibilityConverter();

		/// <inheritdoc/>
		public object? Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			if (values.Any(v => !(v is bool)))
				return DependencyProperty.UnsetValue;

			return values.Cast<bool>().Any(b => b) ? Visibility.Visible : Visibility.Collapsed;
		}

		/// <inheritdoc/>
		public object[]? ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
