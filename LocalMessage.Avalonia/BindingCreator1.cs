using System.Resources;
using Avalonia.Data;
using LocalMessage.Avalonia;

namespace LocalMessage.Avalonia
{
    internal class BindingCreator1 : BindingCreator
    {
        public string Message { get; }

        public BindingCreator1(string message)
        {
            Message = message;
        }

        public override IBinding Create(ResourceManager manager)
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
