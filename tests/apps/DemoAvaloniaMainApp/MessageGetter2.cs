using LocalMessage.Avalonia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoAvaloniaMainApp
{
    public static class MessageGetter2
    {
        public static string? MessageProp1(string message) => DemoAvaloniaSubTools.MessageGetter.MessageProp1(message);
        public static string? MessageProp2(string message) => DemoAvaloniaSubTools.MessageGetter.MessageProp2(message);
        public static string? MessageProp3(string message) => DemoAvaloniaSubTools.MessageGetter.MessageProp3(message);
        public static string? MessageAnoth1(string message) => DemoAvaloniaSubTools.MessageGetter.MessageAnoth1(message);
        public static string? MessageAnoth2(string message) => DemoAvaloniaSubTools.MessageGetter.MessageAnoth2(message);
        public static string? MessageAnoth3(string message) => DemoAvaloniaSubTools.MessageGetter.MessageAnoth3(message);
    }
}
