using Avalonia;

namespace LocalMessage.Avalonia.Utils
{
    public static class PropUtils
    {
        public static string? GetAttachedProperty(object avaloniaObj, AvaloniaProperty<string?> prop)
            => (avaloniaObj as AvaloniaObject)?.GetValue(prop) as string;
    }
}
