using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace PresentationBase.Behaviors
{
    /// <summary>
    /// The <see cref="TextBox"/> will select all content when it is focused.
    /// </summary>
    public class SelectAllOnFocusBehavior
        : Behavior<TextBox>
    {
        /// <summary>
        /// Select all content when <see cref="TextBoxBase.TextChanged"/> was raised for the first time.
        /// </summary>
        public bool SelectAllOnFirstChange { get; set; }

        protected override void OnAttached()
        {
            AssociatedObject.GotFocus -= GotFocusHandler;
            AssociatedObject.GotFocus += GotFocusHandler;

            if (SelectAllOnFirstChange)
                AssociatedObject.TextChanged += TextChangedHandler;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.GotFocus -= GotFocusHandler;
            AssociatedObject.TextChanged -= TextChangedHandler;
        }

        private void GotFocusHandler(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

        private void TextChangedHandler(object sender, TextChangedEventArgs e)
        {
            ((TextBox)sender).TextChanged -= TextChangedHandler;
            ((TextBox)sender).SelectAll();
        }

    }
}
