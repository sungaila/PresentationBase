#nullable enable
using System;
using System.Globalization;
using System.Windows.Data;

namespace PresentationBase.Converters
{
	/// <summary>
	/// Compares a given converter value to <c>null</c> and returns the result.
	/// </summary>
	[ValueConversion(typeof(object), typeof(bool))]
	public class NullToBoolConverter
		: IValueConverter
	{
		public static readonly NullToBoolConverter Instance = new NullToBoolConverter();

		public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value == null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
