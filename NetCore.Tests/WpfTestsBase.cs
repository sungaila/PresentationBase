﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.Windows;
[assembly: Parallelize(Scope = ExecutionScope.MethodLevel)]

namespace PresentationBase.Tests
{
    [TestClass]
    public abstract class WpfTestsBase
    {
        protected static Application? App => Application.Current;

        private static Thread? AppThread { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            if (App != null)
                return;

            bool appStarted = false;

            AppThread = new Thread(() =>
            {
                if (App != null)
                    return;

                try
                {
#pragma warning disable CA1806
                    new Application();
#pragma warning restore CA1806
                    App!.Startup += (s, e) => appStarted = true;
                    App.Run();
                }
                catch (Exception)
                {
                    appStarted = true;
                }
            });
            AppThread.SetApartmentState(ApartmentState.STA);
            AppThread.Start();

            while (!appStarted) { }
        }

        protected static void CreateInvisibleMainWindow()
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