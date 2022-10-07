using System.Resources;

#if IS_AVALONIA
using BindingBase = Avalonia.Data.IBinding;

namespace LocalMessage.Avalonia
#endif

#if IS_WPF
using System.Windows.Data;

namespace LocalMessage.WPF
#endif
{
    internal abstract class BindingCreator
    {
        public abstract BindingBase Create(ResourceManager manager);
    }
}
