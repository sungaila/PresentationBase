using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace PresentationBase.Converters
{
	/// <summary>
	/// Returns <see cref="Visibility.Visible"/> when the given converter value is a <c>0</c> (<see cref="int"/>).
	/// Otherwise <see cref="Visibility.Collapsed"/>.
	/// </summary>
	[ValueConversion(typeof(int), typeof(Visibility))]
	public class ZeroToVisibilityConverter
		: IValueConverter
	{
		/// <summary>
		/// A static instance of this value converter.
		/// </summary>
		public static readonly ZeroToVisibilityConverter Instance = new ZeroToVisibilityConverter();

		/// <inheritdoc/>
		public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is int))
				return Visibility.Collapsed;

			return (int)value == 0 ? Visibility.Visible : Visibility.Collapsed;
		}

		/// <inheritdoc/>
		public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
