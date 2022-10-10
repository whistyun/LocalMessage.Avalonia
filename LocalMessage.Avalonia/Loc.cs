using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LocalMessage.Avalonia
{
    public static class Loc
    {
        public static string? Message(string message)
        {
            var trace = new StackTrace();
            var caller = trace.GetFrames()
                                 .Skip(1)
                                 .Select(f => f?.GetMethod()?.DeclaringType?.Assembly)
                                 .OfType<Assembly>()
                                 .FirstOrDefault();

            return Message(caller, message);
        }

        public static string? Message(string rsource, string message)
        {
            var trace = new StackTrace();
            var caller = trace.GetFrames()
                                 .Skip(1)
                                 .Select(f => f?.GetMethod()?.DeclaringType?.Assembly)
                                 .OfType<Assembly>()
                                 .FirstOrDefault();

            return Message(caller, rsource, message);
        }

        public static string? Message(Assembly resourceHolder, string message)
        {
            var manager = MessageService.GetResource(new ResourceKey(resourceHolder));
            return manager.GetString(message, MessageService.Instance.Culture);
        }

        public static string? Message(Assembly resourceHolder, string resource, string message)
        {
            var manager = MessageService.GetResource(new ResourceKey(resourceHolder, resource));
            return manager.GetString(message, MessageService.Instance.Culture);
        }
    }
}
