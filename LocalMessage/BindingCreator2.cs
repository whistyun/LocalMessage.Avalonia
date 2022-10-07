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
    internal class BindingCreator2 : BindingCreator
    {
        public Binding MessageBinding { get; }

        public BindingCreator2(Binding messageBinding)
        {
            MessageBinding = messageBinding;
        }

        public override BindingBase Create(ResourceManager manager)
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
