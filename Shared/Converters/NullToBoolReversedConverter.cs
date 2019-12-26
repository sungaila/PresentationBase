using System;
using System.Globalization;
using System.Windows.Data;

namespace PresentationBase.Converters
{
	/// <summary>
	/// Compares a given converter value to <see langword="null"/> and returns the <strong>negated</strong> result.
	/// </summary>
	[ValueConversion(typeof(object), typeof(bool))]
	public class NullToBoolReversedConverter
		: IValueConverter
	{
		/// <summary>
		/// A static instance of this value converter.
		/// </summary>
		public static readonly NullToBoolReversedConverter Instance = new NullToBoolReversedConverter();

		/// <inheritdoc/>
		public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value != null;
		}

		/// <inheritdoc/>
		public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
