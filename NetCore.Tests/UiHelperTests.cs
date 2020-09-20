using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Controls;

namespace PresentationBase.Tests
{
    [TestClass]
    public class UiHelperTests : WpfTestsBase
    {
        [TestMethod]
        [DoNotParallelize]
        public void TryFindChild()
        {
            App!.Dispatcher.Invoke(() =>
            {
                var grid = new Grid();
                grid.Children.Add(new Button
                {
                    Name = "ButtonToFind"
                });
                grid.Children.Add(new Button
                {
                    Name = "OtherButton"
                });
                grid.Children.Add(new TextBox());

                CreateInvisibleMainWindow();
                App.MainWindow.Content = grid;
                App.MainWindow.Show();

                var foundButton = UiHelper.TryFindChild<Button>(App.MainWindow, "ButtonToFind");
                Assert.IsNotNull(foundButton);
                Assert.AreEqual("ButtonToFind", foundButton!.Name);
                Assert.AreSame(grid.Children[0], foundButton);

                foundButton = UiHelper.TryFindChild<Button>(App.MainWindow, "OtherButton");
                Assert.IsNotNull(foundButton);
                Assert.AreEqual("OtherButton", foundButton!.Name);
                Assert.AreSame(grid.Children[1], foundButton);

                foundButton = UiHelper.TryFindChild<Button>(App.MainWindow, "NotExistingButton");
                Assert.IsNull(foundButton);

                var foundTextBox = UiHelper.TryFindChild<TextBox>(App.MainWindow);
                Assert.IsNotNull(foundTextBox);
                Assert.AreEqual(string.Empty, foundTextBox!.Name);
                Assert.AreSame(grid.Children[2], foundTextBox);
            });
        }
    }
}
