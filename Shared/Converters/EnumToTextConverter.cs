using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace PresentationBase.Converters
{
	/// <summary>
	/// Converts an <see cref="Enum"/> into its name by calling <see cref="Enum.GetName(Type, object)"/>.
	/// </summary>
	[ValueConversion(typeof(Enum), typeof(string))]
	[MarkupExtensionReturnType(typeof(EnumToTextConverter))]
	public class EnumToTextConverter
		: MarkupExtension, IValueConverter
	{
		/// <summary>
		/// A static instance of this value converter.
		/// </summary>
		public static readonly EnumToTextConverter Instance = new EnumToTextConverter();

		/// <inheritdoc/>
		public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is Enum))
				return DependencyProperty.UnsetValue;

			return Enum.GetName(value.GetType(), value);
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
