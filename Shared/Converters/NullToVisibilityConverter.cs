#nullable enable
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace PresentationBase.Converters
{
	/// <summary>
	/// Compares a given converter value to <c>null</c>.
	/// Returns <see cref="Visibility.Visible"/> when <c>true</c>. Otherwise <see cref="Visibility.Collapsed"/>.
	/// </summary>
	[ValueConversion(typeof(object), typeof(Visibility))]
	public class NullToVisibilityConverter
		: IValueConverter
	{
		public static readonly NullToVisibilityConverter Instance = new NullToVisibilityConverter();

		public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value == null ? Visibility.Visible : Visibility.Collapsed;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
