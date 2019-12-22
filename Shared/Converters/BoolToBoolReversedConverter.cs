#nullable enable
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace PresentationBase.Converters
{
	/// <summary>
	/// Negates a given <see cref="bool"/>.
	/// </summary>
	[ValueConversion(typeof(bool), typeof(bool))]
	public class BoolToBoolReversedConverter
		: IValueConverter
	{
		public static readonly BoolToBoolReversedConverter Instance = new BoolToBoolReversedConverter();

		public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is bool))
				return DependencyProperty.UnsetValue;

			return !(bool)value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is bool))
				return DependencyProperty.UnsetValue;

			return !(bool)value;
		}
	}
}
