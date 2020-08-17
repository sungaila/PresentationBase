using Microsoft.VisualStudio.TestTools.UnitTesting;
using PresentationBase.Extensions;
using System.Windows;
using System.Windows.Controls;

namespace PresentationBase.Tests.Extensions
{
    [TestClass]
    public class ButtonExtensionsTests : WpfTestsBase
    {
        [TestMethod]
        [DoNotParallelize]
        public void CommandExecute()
        {
            Initialize();
            App!.Dispatcher.Invoke(() =>
            {
                App.ShutdownMode = ShutdownMode.OnExplicitShutdown;

                var button = new Button();
                ButtonExtensions.SetDialogResult(button, true);

                CreateInvisibleMainWindow();
                App.MainWindow.Content = button;

                Assert.IsTrue(App.MainWindow.DialogResult != true);
                App.MainWindow.Loaded += (s, e) =>
                {
                    button.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                };
                Assert.IsTrue(App.MainWindow.ShowDialog() == true);
            });
        }
    }
}
