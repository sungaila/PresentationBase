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
		: MarkupExtension, IValueConverter
	{
		/// <summary>
		/// A static instance of this value converter.
		/// </summary>
		public static readonly BoolToBoolReversedConverter Instance = new BoolToBoolReversedConverter();

		/// <inheritdoc/>
		public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is bool))
				return DependencyProperty.UnsetValue;

			return !(bool)value;
		}

		/// <inheritdoc/>
		public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is bool))
				return DependencyProperty.UnsetValue;

			return !(bool)value;
		}

		/// <inheritdoc/>
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return Instance;
		}
	}
}
