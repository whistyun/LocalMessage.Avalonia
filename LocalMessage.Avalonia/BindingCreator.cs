using Avalonia.Data;
using System.Resources;

namespace LocalMessage.Avalonia
{
    internal abstract class BindingCreator
    {
        public abstract IBinding Create(ResourceManager manager);
    }
}
