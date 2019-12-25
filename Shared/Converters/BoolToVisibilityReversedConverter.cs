using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace PresentationBase.Converters
{
	/// <summary>
	/// Returns <see cref="Visibility.Collapsed"/> when converting <c>true</c>. Otherwise <see cref="Visibility.Visible"/>.
	/// </summary>
	[ValueConversion(typeof(bool), typeof(Visibility))]
	public class BoolToVisibilityReversedConverter
		: IValueConverter
	{
		/// <summary>
		/// A static instance of this value converter.
		/// </summary>
		public static readonly BoolToVisibilityReversedConverter Instance = new BoolToVisibilityReversedConverter();

		/// <inheritdoc/>
		public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is bool))
				return DependencyProperty.UnsetValue;

			return (bool)value ? Visibility.Collapsed : Visibility.Visible;
		}

		/// <inheritdoc/>
		public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is Visibility))
				return DependencyProperty.UnsetValue;

			return (Visibility)value == Visibility.Visible ? false : true;
		}
	}
}
