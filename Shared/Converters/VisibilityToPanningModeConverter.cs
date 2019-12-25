using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace PresentationBase.Converters
{
	/// <summary>
	/// Returns the given <see cref="Binding.ConverterParameter"/> value when the given conterter value is <see cref="Visibility.Visible"/>.
	/// Otherwise <see cref="PanningMode.None"/>.
	/// </summary>
	[ValueConversion(typeof(Visibility), typeof(PanningMode), ParameterType = typeof(PanningMode))]
	public class VisibilityToPanningModeConverter
		: IValueConverter
	{
		/// <summary>
		/// A static instance of this value converter.
		/// </summary>
		public static readonly VisibilityToPanningModeConverter Instance = new VisibilityToPanningModeConverter();

		/// <inheritdoc/>
		public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is Visibility))
				return DependencyProperty.UnsetValue;

			var scrollBarVisibility = (Visibility)value;

			if (!(parameter is PanningMode))
				return DependencyProperty.UnsetValue;

			var panningMode = (PanningMode)parameter;

			return scrollBarVisibility == Visibility.Visible ? panningMode : PanningMode.None;
		}

		/// <inheritdoc/>
		public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
