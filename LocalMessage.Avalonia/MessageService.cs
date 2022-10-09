using Avalonia;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace LocalMessage.Avalonia
{
    public class MessageService : INotifyPropertyChanged
    {
        private static readonly MessageService s_instance = new();
        public static MessageService Instance => s_instance;

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

        public static readonly AttachedProperty<string?> AssemblyNameProperty
            = AvaloniaProperty.RegisterAttached<MessageService, AvaloniaObject, string?>("AssemblyName", null);

        public static readonly AttachedProperty<string?> ResourceNameProperty
            = AvaloniaProperty.RegisterAttached<MessageService, AvaloniaObject, string?>("ResourceName", null);

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
    }
}
