using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using System;
using System.Threading;

namespace AvaloniaTestHelper
{
    internal class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new StartUpWindow();
            }
            base.OnFrameworkInitializationCompleted();
        }

        public static void Start()
        {
            var th = new Thread(StartAvalonia) { Name = "AvaloniaTestHelper.UI" };
            th.SetApartmentState(ApartmentState.STA);
            th.Start();

            static void StartAvalonia()
            {
                var builder = AppBuilder.Configure<App>();
                builder.UsePlatformDetect();
                builder.StartWithClassicDesktopLifetime(new string[0]);
            }
        }

        public static T Invoke<T>(Func<T> fun)
        {
            for (; ; )
            {
                if (StartUpWindow.WindowSetuped) break;
                Thread.Sleep(100);
            }

            return Dispatcher.UIThread.InvokeAsync(fun).Result;
        }

        public static void WaitReady()
        {
            for (; ; )
            {
                if (StartUpWindow.WindowSetuped) break;
                Thread.Sleep(100);

            }
        }
    }
}
