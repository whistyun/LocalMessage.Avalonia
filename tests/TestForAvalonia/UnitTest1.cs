
using Avalonia;
using Avalonia.Controls;
using Avalonia.Threading;
using Avalonia.VisualTree;
using AvaloniaTestHelper;
using LocalMessage.Avalonia;
using NUnit.Framework;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TestForAvalonia
{
    [TestFixture]
    public class UnitTest1
    {
        public UnitTest1()
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-US");
        }

        [UITest(RunOnUI = false)]
        public void TestFinder(DemoAvaloniaMainApp.ControlLookProps target)
        {
            var textbox1_1 = AvaPath.FindFirst<TextBlock>(target, "ListBox TextBlock:nth-child(1)");
            var textbox1_2 = AvaPath.FindFirst<TextBlock>(target, "ListBox TextBlock#TemplateDirectMsg");
            var textbox1_3 = AvaPath.FindFirst<TextBlock>(target, "ListBox #TemplateDirectMsg");
            Assert.IsTrue(Equals(textbox1_1, textbox1_2));
            Assert.IsTrue(Equals(textbox1_2, textbox1_3));
        }


        [UITest(RunOnUI = false)]
        public void ControlDefaultTest(DemoAvaloniaMainApp.ControlLookProps target)
        {
            MessageService.Instance.Culture = new CultureInfo("en-US");

            Assert.AreEqual("App Props Default 1", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#DirectMsg")));
            Assert.AreEqual("App Props Default 2", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#StyleMsg")));
            Assert.AreEqual("App Props Default 3", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#BindingMsg")));
            Assert.AreEqual("App Props Default 4", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#TemplateDirectMsg")));
            Assert.AreEqual("App Props Default 5", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#TemplateStyleMsg")));
            Assert.AreEqual("App Props Default 6", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#TemplateBindingMsg")));

            MessageService.Instance.Culture = new CultureInfo("ja-JP");

            Assert.AreEqual("App Props ja-JP 1", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#DirectMsg")));
            Assert.AreEqual("App Props ja-JP 2", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#StyleMsg")));
            Assert.AreEqual("App Props ja-JP 3", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#BindingMsg")));
            Assert.AreEqual("App Props ja-JP 4", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#TemplateDirectMsg")));
            Assert.AreEqual("App Props ja-JP 5", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#TemplateStyleMsg")));
            Assert.AreEqual("App Props ja-JP 6", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#TemplateBindingMsg")));
        }

        [UITest(RunOnUI = false)]
        public void WindowDefaultTest(DemoAvaloniaMainApp.WindowControlLookProps target)
        {
            MessageService.Instance.Culture = new CultureInfo("en-US");

            Assert.AreEqual("App Props Default 1", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#DirectMsg")));
            Assert.AreEqual("App Props Default 2", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#StyleMsg")));
            Assert.AreEqual("App Props Default 3", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#BindingMsg")));
            Assert.AreEqual("App Props Default 4", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#TemplateDirectMsg")));
            Assert.AreEqual("App Props Default 5", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#TemplateStyleMsg")));
            Assert.AreEqual("App Props Default 6", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#TemplateBindingMsg")));

            MessageService.Instance.Culture = new CultureInfo("ja-JP");

            Assert.AreEqual("App Props ja-JP 1", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#DirectMsg")));
            Assert.AreEqual("App Props ja-JP 2", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#StyleMsg")));
            Assert.AreEqual("App Props ja-JP 3", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#BindingMsg")));
            Assert.AreEqual("App Props ja-JP 4", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#TemplateDirectMsg")));
            Assert.AreEqual("App Props ja-JP 5", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#TemplateStyleMsg")));
            Assert.AreEqual("App Props ja-JP 6", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#TemplateBindingMsg")));
        }

        [UITest(RunOnUI = false)]
        public void ControlAnotherTest(DemoAvaloniaMainApp.ControlLookAnoth target)
        {
            MessageService.Instance.Culture = new CultureInfo("en-US");

            Assert.AreEqual("App Another Default 1", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#DirectMsg")));
            Assert.AreEqual("App Another Default 2", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#StyleMsg")));
            Assert.AreEqual("App Another Default 3", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#BindingMsg")));
            Assert.AreEqual("App Another Default 4", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#TemplateDirectMsg")));
            Assert.AreEqual("App Another Default 5", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#TemplateStyleMsg")));
            Assert.AreEqual("App Another Default 6", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#TemplateBindingMsg")));

            MessageService.Instance.Culture = new CultureInfo("ja-JP");

            Assert.AreEqual("App Another ja-JP 1", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#DirectMsg")));
            Assert.AreEqual("App Another ja-JP 2", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#StyleMsg")));
            Assert.AreEqual("App Another ja-JP 3", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#BindingMsg")));
            Assert.AreEqual("App Another ja-JP 4", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#TemplateDirectMsg")));
            Assert.AreEqual("App Another ja-JP 5", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#TemplateStyleMsg")));
            Assert.AreEqual("App Another ja-JP 6", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#TemplateBindingMsg")));
        }

        [UITest(RunOnUI = false)]
        public void WindowAnotherTest(DemoAvaloniaMainApp.WindowControlLookAnoth target)
        {
            MessageService.Instance.Culture = new CultureInfo("en-US");

            Assert.AreEqual("App Another Default 1", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#DirectMsg")));
            Assert.AreEqual("App Another Default 2", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#StyleMsg")));
            Assert.AreEqual("App Another Default 3", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#BindingMsg")));
            Assert.AreEqual("App Another Default 4", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#TemplateDirectMsg")));
            Assert.AreEqual("App Another Default 5", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#TemplateStyleMsg")));
            Assert.AreEqual("App Another Default 6", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#TemplateBindingMsg")));

            MessageService.Instance.Culture = new CultureInfo("ja-JP");

            Assert.AreEqual("App Another ja-JP 1", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#DirectMsg")));
            Assert.AreEqual("App Another ja-JP 2", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#StyleMsg")));
            Assert.AreEqual("App Another ja-JP 3", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#BindingMsg")));
            Assert.AreEqual("App Another ja-JP 4", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#TemplateDirectMsg")));
            Assert.AreEqual("App Another ja-JP 5", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#TemplateStyleMsg")));
            Assert.AreEqual("App Another ja-JP 6", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#TemplateBindingMsg")));
        }


        [UITest(RunOnUI = false)]
        public void LibControlDefaultTest(DemoAvaloniaMainApp.LibControlLookProps target)
        {
            MessageService.Instance.Culture = new CultureInfo("en-US");

            Assert.AreEqual("Lib Props Default 1", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#DirectMsg")));
            Assert.AreEqual("Lib Props Default 2", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#StyleMsg")));
            Assert.AreEqual("Lib Props Default 3", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#BindingMsg")));
            Assert.AreEqual("Lib Props Default 4", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#TemplateDirectMsg")));
            Assert.AreEqual("Lib Props Default 5", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#TemplateStyleMsg")));
            Assert.AreEqual("Lib Props Default 6", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#TemplateBindingMsg")));

            MessageService.Instance.Culture = new CultureInfo("ja-JP");

            Assert.AreEqual("Lib Props ja-JP 1", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#DirectMsg")));
            Assert.AreEqual("Lib Props ja-JP 2", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#StyleMsg")));
            Assert.AreEqual("Lib Props ja-JP 3", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#BindingMsg")));
            Assert.AreEqual("Lib Props ja-JP 4", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#TemplateDirectMsg")));
            Assert.AreEqual("Lib Props ja-JP 5", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#TemplateStyleMsg")));
            Assert.AreEqual("Lib Props ja-JP 6", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#TemplateBindingMsg")));
        }

        [UITest(RunOnUI = false)]
        public void LibControlAnotherTest(DemoAvaloniaMainApp.LibControlLookAnoth target)
        {
            MessageService.Instance.Culture = new CultureInfo("en-US");

            Assert.AreEqual("Lib Another Default 1", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#DirectMsg")));
            Assert.AreEqual("Lib Another Default 2", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#StyleMsg")));
            Assert.AreEqual("Lib Another Default 3", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#BindingMsg")));
            Assert.AreEqual("Lib Another Default 4", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#TemplateDirectMsg")));
            Assert.AreEqual("Lib Another Default 5", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#TemplateStyleMsg")));
            Assert.AreEqual("Lib Another Default 6", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#TemplateBindingMsg")));

            MessageService.Instance.Culture = new CultureInfo("ja-JP");

            Assert.AreEqual("Lib Another ja-JP 1", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#DirectMsg")));
            Assert.AreEqual("Lib Another ja-JP 2", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#StyleMsg")));
            Assert.AreEqual("Lib Another ja-JP 3", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#BindingMsg")));
            Assert.AreEqual("Lib Another ja-JP 4", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#TemplateDirectMsg")));
            Assert.AreEqual("Lib Another ja-JP 5", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#TemplateStyleMsg")));
            Assert.AreEqual("Lib Another ja-JP 6", GetText(AvaPath.FindFirst<TextBlock>(target, "TextBlock#TemplateBindingMsg")));
        }














        private void Invoke(Action act)
            => Dispatcher.UIThread.InvokeAsync(act);

        private Task<T> Invoke<T>(Func<T> act)
            => Dispatcher.UIThread.InvokeAsync(act);

        private string FindText(Visual vis, string controlName)
            => Dispatcher.UIThread.InvokeAsync(() =>
            {
                var textblock = vis.Find<TextBlock>(controlName);

                return textblock.Text;
            })
            .Result;

        private string GetText(TextBlock vis)
            => Dispatcher.UIThread.InvokeAsync(() => vis.Text).Result;
    }
}
