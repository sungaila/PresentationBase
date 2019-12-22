using Microsoft.Xaml.Behaviors;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace PresentationBase.Behaviors
{
    /// <summary>
    /// For <see cref="TextBox"/>es that should refuse any user input but natural numbers.
    /// </summary>
    public class AcceptNumbersBehavior
        : Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            AssociatedObject.PreviewTextInput -= PreviewTextInputHandler;
            AssociatedObject.PreviewTextInput += PreviewTextInputHandler;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.PreviewTextInput -= PreviewTextInputHandler;
        }

        private static readonly Regex _numberRegex = new Regex(@"^\d+$", RegexOptions.Compiled);

        private void PreviewTextInputHandler(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !_numberRegex.IsMatch(e.Text);
        }
    }
}
