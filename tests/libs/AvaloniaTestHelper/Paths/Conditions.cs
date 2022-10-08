using Avalonia;
using Avalonia.VisualTree;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaTestHelper.Paths
{
    interface ICondition
    {
        void Setup(Func<string?, string, Type> typeResolver);
        bool Match(IReadOnlyList<IVisual> siblings, IVisual target);
    }

    class And : ICondition
    {
        public IReadOnlyCollection<ICondition> Conditions { get; }

        public And(params ICondition[] conditions)
        {
            Conditions = conditions.ToArray();
        }

        public And(IEnumerable<ICondition> conditions)
        {
            Conditions = conditions.ToArray();
        }

        public void Setup(Func<string?, string, Type> typeResolver)
        {
            foreach (var cnd in Conditions)
                cnd.Setup(typeResolver);
        }

        public bool Match(IReadOnlyList<IVisual> siblings, IVisual target)
        {
            foreach (var cnd in Conditions)
                if (!cnd.Match(siblings, target))
                    return false;

            return true;
        }
    }

    class OfType : ICondition
    {
        public string? NamespaceName { get; }
        public string TypeName { get; }
        public Type? Type { set; get; }

        public OfType(string typeName) : this(null, typeName) { }

        public OfType(string? @namespaceName, string typeName)
        {
            NamespaceName = namespaceName;
            TypeName = typeName;
        }

        public void Setup(Func<string?, string, Type> typeResolver)
        {
            Type = typeResolver(NamespaceName, TypeName);
        }

        public bool Match(IReadOnlyList<IVisual> siblings, IVisual target)
        {
            if (target is null) return false;

            return target.GetType().Equals(Type);
        }
    }

    class IsType : ICondition
    {
        public string? NamespaceName { get; }
        public string TypeName { get; }
        public Type? Type { set; get; }

        public IsType(string typeName) : this(null, typeName) { }

        public IsType(string? @namespaceName, string typeName)
        {
            NamespaceName = namespaceName;
            TypeName = typeName;
        }

        public void Setup(Func<string?, string, Type> typeResolver)
        {
            Type = typeResolver(NamespaceName, TypeName);
        }

        public bool Match(IReadOnlyList<IVisual> siblings, IVisual target)
        {
            if (target is null) return false;
            return Type!.IsAssignableFrom(target.GetType());
        }
    }

    class HasName : ICondition
    {
        public string Name { get; }

        public HasName(string name)
        {
            Name = name;
        }

        public void Setup(Func<string, string, Type> typeResolver) { }

        public bool Match(IReadOnlyList<IVisual> siblings, IVisual target)
        {
            if (target is StyledElement style)
            {
                return style.Name == Name;
            }
            return false;
        }

    }

    class HasClass : ICondition
    {
        public string ClassName { get; }

        public HasClass(string className)
        {
            ClassName = className;
        }

        public void Setup(Func<string, string, Type> typeResolver) { }

        public bool Match(IReadOnlyList<IVisual> siblings, IVisual target)
        {
            if (target is StyledElement style)
            {
                return style.Classes.Contains(ClassName);
            }
            return false;
        }
    }

    class Property : ICondition
    {
        public string PropertyName { get; }
        public string Value { get; }

        public Property(string propName, string value)
        {
            PropertyName = propName;
            Value = value;
        }

        public void Setup(Func<string, string, Type> typeResolver)
        {
        }

        public bool Match(IReadOnlyList<IVisual> siblings, IVisual target)
        {
            var pinfo = target.GetType().GetProperty(PropertyName);
            if (pinfo is null) return false;

            try
            {
                var pType = pinfo.PropertyType;

                object val =
                    pType == typeof(string) ? Value :
                    pType == typeof(int) ? int.Parse(Value) :
                    pType == typeof(float) ? float.Parse(Value) :
                    pType == typeof(double) ? double.Parse(Value) :
                    pType.IsEnum ? Enum.Parse(pType, Value) :
                    throw new ArgumentException();

                return Equals(pinfo.GetValue(target), val);
            }
            catch (Exception e)
            {
#if DEBUG
                throw e;
#else
                Debug.Print(e.Message);
                return false;
#endif
            }
        }
    }

    class NthChild : ICondition
    {
        public int Offset { get; }
        public int Step { get; }

        public NthChild(int offset) : this(0, offset) { }
        public NthChild(int step, int offset)
        {
            Offset = offset;
            Step = step;
        }

        public void Setup(Func<string, string, Type> typeResolver) { }

        public bool Match(IReadOnlyList<IVisual> siblings, IVisual target)
        {
            for (int i = 0; i < siblings.Count; ++i)
            {
                var sibling = siblings[i];

                if (Equals(sibling, target))
                {
                    var idx = i + 1;

                    return Step == 0 ? idx == Offset : idx % Step == Offset;
                }
            }

            return false;
        }
    }

    class LastNthChild : ICondition
    {
        public int Offset { get; }
        public int Step { get; }

        public LastNthChild(int offset) : this(0, offset) { }
        public LastNthChild(int step, int offset)
        {
            Offset = offset;
            Step = step;
        }

        public void Setup(Func<string, string, Type> typeResolver) { }

        public bool Match(IReadOnlyList<IVisual> siblings, IVisual target)
        {
            for (int i = 0; i < siblings.Count; ++i)
            {
                var sibling = siblings[i];

                if (Equals(sibling, target))
                {
                    var idx = Math.Abs(i - siblings.Count);

                    return Step == 0 ? idx == Offset : idx % Step == Offset;
                }
            }

            return false;
        }
    }
}
