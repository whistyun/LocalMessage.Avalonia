using LocalMessage.Avalonia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoAvaloniaMainApp
{
    public static class MessageGetter
    {
        public static string? MessageProp1(string message)
            => Loc.Message(message);

        public static string? MessageProp2(string message)
            => new Func<string?>(() => Loc.Message(message)).Invoke();

        public static string? MessageProp3(string message)
            => Task.Run(() => Loc.Message(message)).Result;

        public static string? MessageAnoth1(string message)
            => Loc.Message("DemoAvaloniaSubTools.Another.Resource", message);

        public static string? MessageAnoth2(string message)
            => new Func<string?>(() => Loc.Message("DemoAvaloniaSubTools.Another.Resource", message)).Invoke();

        public static string? MessageAnoth3(string message)
            => Task.Run(() => Loc.Message("DemoAvaloniaSubTools.Another.Resource", message)).Result;
    }
}
