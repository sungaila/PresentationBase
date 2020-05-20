using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace PresentationBase.Converters
{
	/// <summary>
	/// Compares a given converter value to <see langword="null"/>.
	/// Returns <see cref="Visibility.Collapsed"/> when <c>true</c>. Otherwise <see cref="Visibility.Visible"/>.
	/// </summary>
	[ValueConversion(typeof(object), typeof(Visibility))]
	[MarkupExtensionReturnType(typeof(NullToVisibilityReversedConverter))]
	public class NullToVisibilityReversedConverter
		: ConverterBase
	{
		/// <inheritdoc/>
		public override object? Convert(object? value, Type? targetType, object? parameter, CultureInfo? culture)
		{
			return value == null ? Visibility.Collapsed : Visibility.Visible;
		}
	}
}
