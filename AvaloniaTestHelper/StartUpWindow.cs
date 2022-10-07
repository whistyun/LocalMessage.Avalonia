using Avalonia.Controls;

using System;

namespace AvaloniaTestHelper
{
    internal class StartUpWindow : Window
    {
        public static bool WindowSetuped { private set; get; }

        static StartUpWindow()
        {
            App.Start();
        }

        public StartUpWindow()
        {
            Title = "AvaloniaTestHelper";
            Bounds = new Avalonia.Rect(0, 0, 10, 10);
            WindowState = WindowState.Minimized;
        }

        protected override void OnOpened(EventArgs e)
        {
            base.OnOpened(e);

            WindowSetuped = true;
        }
    }
}
