using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

#if IS_AVALONIA
using Avalonia;
#endif

#if IS_WPF
#endif


namespace LocalMessage.Utils
{
    public static class PropUtils
    {
#if IS_AVALONIA
        public static string? GetAttachedProperty(object avaloniaObj, AvaloniaProperty<string?> prop)
            => (avaloniaObj as AvaloniaObject).GetValue(prop) as string;
#endif
#if IS_WPF
        public static string? GetAttachedProperty(object dependencyObj, DependencyProperty prop)
            => (dependencyObj as DependencyObject).GetValue(prop) as string;
#endif


    }
}
