using System.Windows;
using System.Windows.Controls;

namespace PresentationBase.Extensions
{
    /// <summary>
    /// Provides attached properties for <see cref="Button"/>s.
    /// </summary>
    public static class ButtonExtensions
    {
        /// <summary>
        /// Clicking the <see cref="Button"/> will close its parent <see cref="Window"/> with the given <see cref="Window.DialogResult"/>.
        /// </summary>
        public static readonly DependencyProperty DialogResultProperty = DependencyProperty.RegisterAttached(
            "DialogResult",
            typeof(bool?),
            typeof(ButtonExtensions),
            new FrameworkPropertyMetadata(null, DialogResultPropertyChanged));

        private static void DialogResultPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is Button button))
                return;

            button.Click -= ButtonClickHandler;
            button.Click += ButtonClickHandler;
        }

        private static void ButtonClickHandler(object sender, RoutedEventArgs e)
        {
            Window.GetWindow((Button)sender).DialogResult = GetDialogResult((Button)sender);
        }

        /// <summary>
        /// Get the value of the attached property <see cref="DialogResultProperty"/> for a <see cref="Button"/>.
        /// </summary>
        /// <param name="button">The button.</param>
        public static bool? GetDialogResult(Button button)
        {
            return (bool?)button.GetValue(DialogResultProperty);
        }

        /// <summary>
        /// Set the value of the attached property <see cref="DialogResultProperty"/> for a <see cref="Button"/>.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <param name="value">The value to set.</param>
        public static void SetDialogResult(Button button, bool? value)
        {
            button.SetValue(DialogResultProperty, value);
        }

        /// <summary>
        /// Contains a glyph from the <strong>Segoe MDL2 Assets</strong> font.
        /// </summary>
        public static readonly DependencyProperty SegoeGlyphProperty = DependencyProperty.RegisterAttached(
            "SegoeGlyph",
            typeof(string),
            typeof(ButtonExtensions),
            new FrameworkPropertyMetadata(null));

        /// <summary>
        /// Get the value of the attached property <see cref="SegoeGlyphProperty"/> for a <see cref="Button"/>.
        /// </summary>
        /// <param name="button">The button.</param>
        public static string GetSegoeGlyph(Button button)
        {
            return (string)button.GetValue(SegoeGlyphProperty);
        }

        /// <summary>
        /// Set the value of the attached property <see cref="SegoeGlyphProperty"/> for a <see cref="Button"/>.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <param name="value">The value to set.</param>
        public static void SetSegoeGlyph(Button button, string value)
        {
            button.SetValue(SegoeGlyphProperty, value);
        }

        /// <summary>
        /// Contains a font size for the <strong>Segoe MDL2 Assets</strong> font.
        /// Is used in conjunction with <see cref="SegoeGlyphProperty"/>.
        /// </summary>
        public static readonly DependencyProperty SegoeGlyphSizeProperty = DependencyProperty.RegisterAttached(
            "SegoeGlyphSize",
            typeof(double),
            typeof(ButtonExtensions),
            new FrameworkPropertyMetadata(32d));

        /// <summary>
        /// Get the value of the attached property <see cref="SegoeGlyphSizeProperty"/> for a <see cref="Button"/>.
        /// </summary>
        /// <param name="button">The button.</param>
        public static double GetSegoeGlyphSize(Button button)
        {
            return (double)button.GetValue(SegoeGlyphSizeProperty);
        }

        /// <summary>
        /// Set the value of the attached property <see cref="SegoeGlyphSizeProperty"/> for a <see cref="Button"/>.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <param name="value">The value to set.</param>
        public static void SetSegoeGlyphSize(Button button, double value)
        {
            button.SetValue(SegoeGlyphSizeProperty, value);
        }
    }
}
