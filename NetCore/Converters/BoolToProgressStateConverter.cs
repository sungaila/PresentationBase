using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Shell;

namespace PresentationBase.Converters
{
	/// <summary>
	/// Returns <see cref="TaskbarItemProgressState.Indeterminate"/> when converting <c>true</c>. Otherwise <see cref="TaskbarItemProgressState.None"/>.
	/// </summary>
	[ValueConversion(typeof(bool), typeof(TaskbarItemProgressState))]
	[MarkupExtensionReturnType(typeof(BoolToProgressStateConverter))]
	public class BoolToProgressStateConverter
		: ConverterBase
	{
		/// <inheritdoc/>
		public override object? Convert(object? value, Type? targetType, object? parameter, CultureInfo? culture)
		{
			if (!(value is bool))
				return DependencyProperty.UnsetValue;

			return (bool)value ? TaskbarItemProgressState.Indeterminate : TaskbarItemProgressState.None;
		}

		/// <inheritdoc/>
		public override object? ConvertBack(object? value, Type? targetType, object? parameter, CultureInfo? culture)
		{
			if (!(value is TaskbarItemProgressState))
				return DependencyProperty.UnsetValue;

			return (TaskbarItemProgressState)value == TaskbarItemProgressState.Indeterminate;
		}
	}
}
