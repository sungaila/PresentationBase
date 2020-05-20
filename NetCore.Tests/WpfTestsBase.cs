using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Windows;

namespace PresentationBase.Tests
{
    public abstract class WpfTestsBase
    {
        protected Application? App { get; private set; }

        private Thread? AppThread { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            bool appStarted = false;

            AppThread = new Thread(() =>
            {
                App = new Application();
                App.Startup += (s, e) => appStarted = true;
                App.Run();
            });
            AppThread.SetApartmentState(ApartmentState.STA);
            AppThread.Start();

            while (!appStarted) { }
        }

        protected void CreateInvisibleMainWindow()
        {
            App!.MainWindow = new Window
            {
                ShowInTaskbar = false,
                ShowActivated = false,
                Width = 0,
                Height = 0,
                WindowStyle = WindowStyle.None
            };
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (App != null)
            {
                App.Dispatcher.Invoke(() => App.Shutdown());
                App = null;
            }

            if (AppThread != null)
            {
                AppThread.Join();
                AppThread = null;
            }
        }
    }
}
