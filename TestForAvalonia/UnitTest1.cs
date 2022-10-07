
using Avalonia;
using Avalonia.Controls;
using AvaloniaTestHelper;
using NUnit.Framework;
using System.Threading;

namespace TestForAvalonia
{
    [TestFixture]
    public class UnitTest1
    {
        [UITest(RunOnUI = true)]
        public void ControlTest(DemoAvaloniaMainApp.MainWindow target)
        {
            var tab = target.FindControl<TabControl>("Tab");
            tab.SelectedIndex = 1;
        }
    }
}
