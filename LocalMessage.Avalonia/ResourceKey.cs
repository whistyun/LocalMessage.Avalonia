using System;
using System.Linq;
using System.Reflection;

namespace LocalMessage.Avalonia
{
    internal struct ResourceKey : IEquatable<ResourceKey>
    {
        public Assembly? Assembly { get; }
        public string? AssemblyName { get; }
        public string Resource { get; }

        public ResourceKey(Assembly assembly)
        {
            Assembly = assembly;
            AssemblyName = assembly.GetName().Name;
            Resource = $"{AssemblyName}.Properties.Resource";
        }

        public ResourceKey(Assembly assembly, string resource)
        {
            Assembly = assembly;
            AssemblyName = assembly.GetName().Name;
            Resource = resource;
        }

        public ResourceKey(string assembly, string resource)
        {
            Assembly = null;
            AssemblyName = assembly;
            Resource = resource;
        }

        public override int GetHashCode()
        {
            int hash = Resource.GetHashCode();

            if (Assembly is not null)
                hash = hash * 32 + Assembly.GetHashCode();

            if (AssemblyName is not null)
                hash = hash * 32 + AssemblyName.GetHashCode();

            return hash;
        }

        public override bool Equals(object? obj)
            => obj is ResourceKey key ? Equals(key) : false;

        public bool Equals(ResourceKey other)
            => Equals(Assembly, other.Assembly)
            && Equals(AssemblyName, other.AssemblyName)
            && Equals(Resource, other.Resource);
    }
}
