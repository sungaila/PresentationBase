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
        : ConverterBase
    {
        /// <inheritdoc/>
        public override object? Convert(object? value, Type? targetType, object? parameter, CultureInfo? culture)
        {
            if (!(value is Enum))
                return DependencyProperty.UnsetValue;

            return Enum.GetName(value.GetType(), value);
        }
    }
}
