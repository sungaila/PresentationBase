#nullable enable
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace PresentationBase.Converters
{
	/// <summary>
	/// Compares a given converter value to <c>null</c>.
	/// Returns <see cref="Visibility.Collapsed"/> when <c>true</c>. Otherwise <see cref="Visibility.Visible"/>.
	/// </summary>
	[ValueConversion(typeof(object), typeof(Visibility))]
	public class NullToVisibilityReversedConverter
		: IValueConverter
	{
		public static readonly NullToVisibilityReversedConverter Instance = new NullToVisibilityReversedConverter();

		public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value == null ? Visibility.Collapsed : Visibility.Visible;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
