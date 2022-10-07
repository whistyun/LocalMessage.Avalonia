using Avalonia;
using Avalonia.Controls;
using Avalonia.Threading;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Builders;
using NUnit.Framework.Internal.Commands;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AvaloniaTestHelper
{
    [System.AttributeUsage(System.AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class UITestAttribute : Attribute, ISimpleTestBuilder, IApplyToTest, IWrapTestMethod
    {
        private static ConcurrentDictionary<object, WindowLook> s_control2Lookup = new();

        private readonly NUnitTestCaseBuilder _builder = new();

        public bool RunOnUI { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }

        public TestMethod BuildFrom(IMethodInfo method, Test suite)
        {
            var methodparams = method.GetParameters();

            if (methodparams.Length == 1)
            {
                App.WaitReady();
                var winType = typeof(Window);
                var visType = typeof(Visual);

                var argType = methodparams[0].ParameterType;

                var look =
                     winType.IsAssignableFrom(argType) ?
                         App.Invoke(() =>
                         {
                             var win = (Window)Activator.CreateInstance(argType);
                             return new WindowLook(win, win);
                         }) :
                     visType.IsAssignableFrom(argType) ?
                         App.Invoke(() =>
                         {
                             var vis = (Visual)Activator.CreateInstance(argType);
                             var win = new Window();
                             win.Content = vis;

                             return new WindowLook(win, vis);
                         }) :
                     throw new ArgumentException($"unsupported argtype {argType.Name}");

                s_control2Lookup[look.Target] = look;

                var parameter = new TestCaseParameters(new[] { look.Target });
                return _builder.BuildTestMethod(method, suite, parameter);
            }
            else
            {
                return _builder.BuildTestMethod(method, suite, null);
            }
        }

        public void ApplyToTest(Test test)
        {
            if (!test.Properties.ContainsKey(PropertyNames.Description) && Description != null)
                test.Properties.Set(PropertyNames.Description, Description);

            if (!test.Properties.ContainsKey(PropertyNames.Author) && Author != null)
                test.Properties.Set(PropertyNames.Author, Author);
        }

        public TestCommand Wrap(TestCommand command)
        {
            return new RunOnUICommand(RunOnUI, command);
        }

        class WindowLook
        {
            internal bool _isOpened;
            internal DateTime _arrangedTime = new();

            public Window Window { get; }
            public object Target { get; }
            public bool Ready => _isOpened && _arrangedTime + new TimeSpan(0, 0, 1) < DateTime.Now;

            public WindowLook(Window win, object target)
            {
                Window = win;
                Target = target;

                Window.Opened += Window_Opened;
                Window.PropertyChanged += Window_PropertyChanged;

                _arrangedTime = DateTime.Now;
                Window.Show();
            }

            private void Window_Opened(object sender, EventArgs e)
            {
                _isOpened = true;
            }

            private void Window_PropertyChanged(object sender, AvaloniaPropertyChangedEventArgs e)
            {
                if (e.Property == Visual.BoundsProperty)
                {
                    _arrangedTime = DateTime.Now;
                }
            }

            public void WaitReady()
            {
                while (!Ready)
                {
                    Thread.Sleep(100);
                    continue;
                }
            }
        }

        class RunOnUICommand : DelegatingTestCommand
        {
            public bool RunOnUIThread { get; }

            public RunOnUICommand(bool runOnUiThread, TestCommand innerCommand)
                : base(innerCommand)
            {
                RunOnUIThread = runOnUiThread;
            }

            public override TestResult Execute(TestExecutionContext context)
            {
                App.WaitReady();

                Window? win = null;

                if (context.CurrentTest.Arguments.Length == 1
                    && s_control2Lookup.TryGetValue(context.CurrentTest.Arguments[0], out var look))
                {
                    look.WaitReady();
                    win = look.Window;
                }

                try
                {
                    if (RunOnUIThread)
                    {
                        var result = Dispatcher.UIThread.InvokeAsync(() => RunTest(context)).Result;

                        if (result is Exception ex)
                            throw ex;

                        return (TestResult)result;
                    }

                    return innerCommand.Execute(context);
                }
                finally
                {
                    Dispatcher.UIThread.InvokeAsync(() => win?.Close());
                }
            }

            private object RunTest(TestExecutionContext context)
            {
                try
                {
                    return innerCommand.Execute(context);
                }
                catch (Exception e)
                {
                    return e;
                }
            }
        }
    }
}
