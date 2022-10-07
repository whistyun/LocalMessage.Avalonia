using System;
using System.Resources;
using System.Globalization;
using System.Collections.Generic;

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
    internal class MessageGetter2 : IMultiValueConverter
    {
        private ResourceManager _manager;

        public MessageGetter2(ResourceManager manager)
        {
            _manager = manager;
        }

        public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo defCulture)
        {
            var culture = values[0] as CultureInfo ?? defCulture;
            var message = values[1] as string;

            return message is null ? "" : _manager.GetString(message, culture);
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo defCulture)
        {
            var culture = values[0] as CultureInfo ?? defCulture;
            var message = values[1] as string;

            return message is null ? "" : _manager.GetString(message, culture);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
