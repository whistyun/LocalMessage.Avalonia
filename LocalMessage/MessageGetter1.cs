using System;
using System.Resources;
using System.Globalization;

#if IS_AVALONIA
using Avalonia;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Markup.Xaml;

namespace LocalMessage.Avalonia
#endif

#if IS_WPF
using System.Windows.Data;

namespace LocalMessage.WPF
#endif
{
    internal class MessageGetter1
    : IValueConverter
    {
        private ResourceManager _manager;

        public MessageGetter1(ResourceManager manager)
        {
            _manager = manager;
        }

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo defCulture)
        {
            var message = parameter as string;
            var culture = value as CultureInfo ?? defCulture;

            return message is null ? "" : _manager.GetString(message, culture);
        }


        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
