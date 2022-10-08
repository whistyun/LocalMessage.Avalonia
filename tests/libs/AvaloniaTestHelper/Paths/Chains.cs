using Avalonia.VisualTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaTestHelper.Paths
{
    enum FindDirection
    {
        Children,
        Descendants,
    }

    class Chain
    {
        public FindDirection Direction { get; set; }
        public ICondition Condition { get; }
        public Chain? Next { get; }

        public Chain(ICondition condition)
        {
            Direction = FindDirection.Descendants;
            Condition = condition;
        }

        public Chain(ICondition condition, Chain? chain)
        {
            Direction = FindDirection.Descendants;
            Condition = condition;
            Next = chain;
        }

        public Chain SetAsChildrenSearch()
        {
            Direction = FindDirection.Children;
            return this;
        }

        public Chain SetAsDescendantSearch()
        {
            Direction = FindDirection.Descendants;
            return this;
        }

        public void Setup(Func<string?, string, Type> typeResolver)
        {
            Condition.Setup(typeResolver);
            Next?.Setup(typeResolver);
        }

        public IEnumerable<IVisual> Find(IVisual target)
        {
            var list =
                Direction == FindDirection.Children ? GetChildren(target) :
                Direction == FindDirection.Descendants ? GetDescendants(target) :
                throw new InvalidOperationException();

            foreach (var vis in list)
            {
                if (Condition.Match(vis.Siblings, vis.Child))
                {
                    if (Next is null)
                        yield return vis.Child;

                    else
                        foreach (var descendant in Next.Find(vis.Child))
                            yield return descendant;
                }
            }

        }

        private static IEnumerable<(IReadOnlyList<IVisual> Siblings, IVisual Child)> GetChildren(IVisual vis)
        {
            foreach (var child in vis.VisualChildren)
            {
                yield return (vis.VisualChildren, child);
            }
        }

        public static IEnumerable<(IReadOnlyList<IVisual> Siblings, IVisual Child)> GetDescendants(IVisual vis)
        {
            foreach (var child in vis.VisualChildren)
            {
                yield return (vis.VisualChildren, child);

                foreach (var descendant in GetDescendants(child))
                    yield return descendant;
            }
        }
    }
}
