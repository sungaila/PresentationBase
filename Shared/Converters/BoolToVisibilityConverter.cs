using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace PresentationBase.Converters
{
	/// <summary>
	/// Returns <see cref="Visibility.Visible"/> when converting <c>true</c>. Otherwise <see cref="Visibility.Collapsed"/>.
	/// </summary>
	[ValueConversion(typeof(bool), typeof(Visibility))]
	[MarkupExtensionReturnType(typeof(BoolToVisibilityConverter))]
	public class BoolToVisibilityConverter
		: MarkupExtension, IValueConverter
	{
		/// <summary>
		/// A static instance of this value converter.
		/// </summary>
		public static readonly BoolToVisibilityConverter Instance = new BoolToVisibilityConverter();

		/// <inheritdoc/>
		public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is bool))
				return DependencyProperty.UnsetValue;

			return (bool)value ? Visibility.Visible : Visibility.Collapsed;
		}

		/// <inheritdoc/>
		public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is Visibility))
				return DependencyProperty.UnsetValue;

			return (Visibility)value == Visibility.Visible ? true : false;
		}

		/// <inheritdoc/>
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return Instance;
		}
	}
}
