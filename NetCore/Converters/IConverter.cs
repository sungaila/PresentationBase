using System;
using System.Windows.Markup;

namespace PresentationBase.Converters
{
    /// <summary>
    /// Every PresentationBase converter contains an instance property and a ProvideValue function.
    /// </summary>
    public interface IConverter
    {
        /// <summary>
        /// A static instance of this value converter.
        /// </summary>
        IConverter Instance { get; }

        /// <summary>
        /// Needed for the implementation of <see cref="MarkupExtension.ProvideValue(IServiceProvider)"/>.
        /// </summary>
        object ProvideValue(IServiceProvider serviceProvider);
    }
}
