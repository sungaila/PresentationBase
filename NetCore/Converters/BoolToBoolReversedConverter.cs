using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace PresentationBase.Converters
{
	/// <summary>
	/// Negates a given <see cref="bool"/>.
	/// </summary>
	[ValueConversion(typeof(bool), typeof(bool))]
	[MarkupExtensionReturnType(typeof(BoolToBoolReversedConverter))]
	public class BoolToBoolReversedConverter
		: ConverterBase
	{
		/// <inheritdoc/>
		public override object? Convert(object? value, Type? targetType, object? parameter, CultureInfo? culture)
		{
			if (!(value is bool))
				return DependencyProperty.UnsetValue;

			return !(bool)value;
		}

		/// <inheritdoc/>
		public override object? ConvertBack(object? value, Type? targetType, object? parameter, CultureInfo? culture)
		{
			if (!(value is bool))
				return DependencyProperty.UnsetValue;

			return !(bool)value;
		}
	}
}
