using System.Resources;

#if IS_AVALONIA
using Avalonia.Data;
using BindingBase = Avalonia.Data.IBinding;

namespace LocalMessage.Avalonia
#endif

#if IS_WPF
using System.Windows.Data;

namespace LocalMessage.WPF
#endif
{
    internal class BindingCreator1 : BindingCreator
    {
        public string Message { get; }

        public BindingCreator1(string message)
        {
            Message = message;
        }

        public override BindingBase Create(ResourceManager manager)
        {
            return new Binding(nameof(MessageService.Culture))
            {
                Source = MessageService.Instance,
                Converter = new MessageGetter1(manager),
                ConverterParameter = Message,
            };
        }
    }

}
