using System;

namespace LocalMessage.Avalonia
{
    internal struct ResourceKey : IEquatable<ResourceKey>
    {
        public string Assembly { get; }
        public string Resource { get; }

        public ResourceKey(string assembly, string resource)
        {
            Assembly = assembly;
            Resource = resource;
        }

        public override int GetHashCode()
        {
            return Assembly.GetHashCode() * 32 + Resource.GetHashCode();
        }

        public override bool Equals(object obj)
            => obj is ResourceKey key ? Equals(key) : false;

        public bool Equals(ResourceKey other)
            => Assembly.Equals(other.Assembly) && Resource.Equals(other.Resource);
    }
}
