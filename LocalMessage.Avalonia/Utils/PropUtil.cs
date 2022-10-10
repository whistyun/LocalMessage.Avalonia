using Avalonia;

namespace LocalMessage.Avalonia.Utils
{
    public static class PropUtil
    {
        public static string? GetAttachedProperty(object avaloniaObj, AvaloniaProperty<string?> prop)
            => (avaloniaObj as AvaloniaObject)?.GetValue(prop) as string;
    }
}
