using System;

namespace LocalMessage.Avalonia.Utils
{
    internal static class SearviceProviderExt
    {
        public static T Find<T>(this IServiceProvider provider) where T : class
            => provider.GetService(typeof(T)) as T ?? throw new ArgumentException($"ServiceProvider can't provide {typeof(T)}");
    }
}
