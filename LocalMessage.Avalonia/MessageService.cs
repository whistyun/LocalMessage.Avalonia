using Avalonia;
using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;

namespace LocalMessage.Avalonia
{
    public class MessageService : INotifyPropertyChanged
    {
        private static ConcurrentDictionary<ResourceKey, ResourceManager> s_cache = new();

        public static MessageService Instance { get; } = new();

        public static readonly AttachedProperty<string?> AssemblyNameProperty
            = AvaloniaProperty.RegisterAttached<MessageService, AvaloniaObject, string?>("AssemblyName", null);

        public static readonly AttachedProperty<string?> ResourceNameProperty
            = AvaloniaProperty.RegisterAttached<MessageService, AvaloniaObject, string?>("ResourceName", null);

        internal static ResourceManager GetResource(ResourceKey key)
        {
            return s_cache.GetOrAdd(key, Load);

            static ResourceManager Load(ResourceKey key)
            {
                var asm = key.Assembly;
                var asmNm = key.AssemblyName;
                var rscNm = key.Resource;

                if (asm is null && asmNm is not null)
                {
                    asm = AppDomain.CurrentDomain
                                   .GetAssemblies()
                                   .FirstOrDefault(asm => asmNm.Equals(asm.GetName().Name, StringComparison.InvariantCultureIgnoreCase));

                    // assembly is not loaded yet.
                    try { asm ??= Assembly.Load(asmNm); }
                    catch { }
                }


                if (asm is null)
                {
                    throw new InvalidOperationException($"Failed to load Assembly '{asmNm}'");
                }

                if (asm.GetType(rscNm) is null)
                {
                    throw new InvalidOperationException($"Failed to load '{rscNm}' type in '{asmNm}' assembly");
                }

                return new ResourceManager(rscNm, asm);
            }
        }

        public static void SetAssemblyName(AvaloniaObject element, string assemblyName)
        {
            element.SetValue(AssemblyNameProperty, assemblyName);
        }

        public static string? SetAssemblyName(AvaloniaObject element)
        {
            return element.GetValue(AssemblyNameProperty) as string;
        }

        public static void SetResourceName(AvaloniaObject element, string resourceName)
        {
            element.SetValue(ResourceNameProperty, resourceName);
        }

        public static string? GetResourceName(AvaloniaObject element)
        {
            return element.GetValue(ResourceNameProperty) as string;
        }




        private string _cultureName;
        private CultureInfo _culture;

        public MessageService()
        {
            _culture = CultureInfo.CurrentCulture;
            _cultureName = _culture.Name;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public CultureInfo Culture
        {
            get => _culture;
            set => TryFire(ref _culture, value);
        }

        public string CultureName
        {
            get => _cultureName;
            set => TryFire(ref _cultureName, value);
        }

        private void TryFire<T>(ref T variable, T value, [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(variable, value)) return;

            variable = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
