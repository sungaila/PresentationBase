using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace PresentationBase.Converters
{
	/// <summary>
	/// Compares a given converter value with the given <see cref="Binding.ConverterParameter"/> value.
	/// Returns <see cref="Visibility.Visible"/> when both values are equal. Otherwise <see cref="Visibility.Collapsed"/>.
	/// </summary>
	[ValueConversion(typeof(bool), typeof(Visibility), ParameterType = typeof(object))]
	[MarkupExtensionReturnType(typeof(EqualsToVisibilityConverter))]
	public class EqualsToVisibilityConverter
		: ConverterBase
	{
		/// <inheritdoc/>
		public override object? Convert(object? value, Type? targetType, object? parameter, CultureInfo? culture)
		{
			if (value == null && parameter == null)
				return Visibility.Visible;

			if (value != null)
				return value.Equals(parameter) ? Visibility.Visible : Visibility.Collapsed;

			return parameter == null ? Visibility.Visible : Visibility.Collapsed;
		}
	}
}
