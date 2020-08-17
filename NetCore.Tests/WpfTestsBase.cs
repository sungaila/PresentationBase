using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Windows;

namespace PresentationBase.Tests
{
    [TestClass]
    public class WpfTestsBase
    {
        private static object AppLock = new object();

        private static bool Initialized;

        protected static Application? App => Application.Current;

        private static Thread? AppThread { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            lock (AppLock)
            {
                if (Initialized)
                    return;
            }

            bool appStarted = false;

            AppThread = new Thread(() =>
            {
                lock (AppLock)
                {
                    if (Initialized || App != null)
                        return;
                }

                new Application();
                App!.Startup += (s, e) => appStarted = true;
                App.Run();
            });
            AppThread.SetApartmentState(ApartmentState.STA);
            AppThread.Start();

            while (!appStarted) { }

            lock (AppLock)
            {
                Initialized = true;
            }
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
            Application.Current?.Dispatcher.Invoke(() => Application.Current?.MainWindow?.Close());
        }
    }
}
