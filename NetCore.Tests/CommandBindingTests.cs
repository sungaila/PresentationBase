using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Controls;
using System.Windows.Data;

namespace PresentationBase.Tests
{
    [TestClass]
    public class CommandBindingTests : WpfTestsBase
    {
        [TestMethod]
        [DoNotParallelize]
        public void CommandExecute()
        {
            App!.Dispatcher.Invoke(() =>
            {
                var viewModel = new TestViewModel();
                var button = new Button
                {
                    DataContext = viewModel
                };

                CreateInvisibleMainWindow();
                App.MainWindow.Content = button;
                App.MainWindow.Show();

                Assert.IsTrue(button.IsEnabled);

                BindingOperations.SetBinding(button, Button.CommandParameterProperty, new Binding());
                BindingOperations.SetBinding(button, Button.CommandProperty, new CommandBinding(typeof(TestCommand)));
                Assert.IsNotNull(button.CommandParameter);
                Assert.IsNotNull(button.Command);
                Assert.IsInstanceOfType(button.Command, typeof(TestCommand));

                Assert.IsFalse(button.IsEnabled);
                Assert.IsTrue(((TestCommand)button.Command).CanExecuteCalled);
                ((TestCommand)button.Command).CanExecuteCalled = false;

                viewModel.Name = "Adam Jensen";
                Assert.IsTrue(button.IsEnabled);
                Assert.IsTrue(((TestCommand)button.Command).CanExecuteCalled);

                button.Command.Execute(button.CommandParameter);
                Assert.IsTrue(((TestCommand)button.Command).ExecuteCalled);

                BindingOperations.SetBinding(button, Button.CommandProperty, new CommandBinding(typeof(DummyCommand)));
                Assert.IsNull(button.Command);
            });
        }

        class TestViewModel : ViewModel
        {
            private string? _name;

            public string? Name
            {
                get => _name;
                set => SetProperty(ref _name, value);
            }
        }

        class TestCommand : ViewModelCommand<TestViewModel>
        {
            public bool CanExecuteCalled { get; set; }

            public bool ExecuteCalled { get; set; }

            public override void Execute(TestViewModel parameter)
            {
                ExecuteCalled = true;
            }

            public override bool CanExecute(TestViewModel parameter)
            {
                CanExecuteCalled = true;
                return base.CanExecute(parameter) && parameter.Name != null;
            }
        }

        class DummyCommand : ViewModelCommand<ViewModel>
        {
            public override void Execute(ViewModel parameter)
            {
                throw new NotImplementedException();
            }
        }
    }
}
