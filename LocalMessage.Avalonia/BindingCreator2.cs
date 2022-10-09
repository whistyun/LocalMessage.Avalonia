using Avalonia.Data;
using System.Resources;
using LocalMessage.Avalonia;

namespace LocalMessage.Avalonia
{
    internal class BindingCreator2 : BindingCreator
    {
        public Binding MessageBinding { get; }

        public BindingCreator2(Binding messageBinding)
        {
            MessageBinding = messageBinding;
        }

        public override IBinding Create(ResourceManager manager)
        {
            var binding = new MultiBinding()
            {
                Converter = new MessageGetter2(manager)
            };
            binding.Bindings.Add(new Binding(nameof(MessageService.Culture)) { Source = MessageService.Instance });
            binding.Bindings.Add(MessageBinding);

            return binding;
        }
    }
}
