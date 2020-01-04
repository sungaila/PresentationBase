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
		: MarkupExtension, IValueConverter
	{
		/// <summary>
		/// A static instance of this value converter.
		/// </summary>
		public static readonly EqualsToVisibilityConverter Instance = new EqualsToVisibilityConverter();

		/// <inheritdoc/>
		public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null && parameter == null)
				return Visibility.Visible;

			if (value != null)
				return value.Equals(parameter) ? Visibility.Visible : Visibility.Collapsed;

			return parameter.Equals(value) ? Visibility.Visible : Visibility.Collapsed;
		}

		/// <inheritdoc/>
		public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc/>
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return Instance;
		}
	}
}
