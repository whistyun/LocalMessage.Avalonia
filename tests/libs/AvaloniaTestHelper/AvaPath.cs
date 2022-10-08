using Avalonia;
using Avalonia.Threading;
using Avalonia.VisualTree;
using AvaloniaTestHelper.Paths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaTestHelper
{

    public class AvaPath
    {
        public static IEnumerable<IVisual> Find(
            IVisual vis,
            string path)
        => Find(vis, path, DefaultFinder);

        public static IEnumerable<IVisual> Find(
            IVisual vis,
            string path,
            Func<string?, string, Type> typeFinder)
        {
            return Dispatcher.UIThread.CheckAccess() ?
                PrivateFind(vis, path, typeFinder) :
                Dispatcher.UIThread.InvokeAsync(() => PrivateFind(vis, path, typeFinder)).Result;
        }

        public static IEnumerable<T> Find<T>(
            IVisual vis,
            string path)
        => Find(vis, path, DefaultFinder).Cast<T>();

        public static IEnumerable<T> Find<T>(
            IVisual vis,
            string path,
            Func<string?, string, Type> typeFinder)
        => Find(vis, path, typeFinder).Cast<T>();

        public static IVisual FindFirst(
            IVisual vis,
            string path)
        => Find(vis, path, DefaultFinder).First();

        public static IVisual FindFirst(
            IVisual vis,
            string path,
            Func<string?, string, Type> typeFinder)
        => Find(vis, path, typeFinder).First();

        public static T FindFirst<T>(
            IVisual vis,
            string path)
        => Find(vis, path, DefaultFinder).Cast<T>().First();

        public static T FindFirst<T>(
            IVisual vis,
            string path,
            Func<string?, string, Type> typeFinder)
        => Find(vis, path, typeFinder).Cast<T>().First();


        private static IEnumerable<IVisual> PrivateFind(
            IVisual vis,
            string path,
            Func<string?, string, Type> typeFinder)
        {
            var parser = new SelectorParser();

            var chain = parser.Parse(path);
            chain.Setup(typeFinder);

            return chain.Find(vis);
        }


        public static Type DefaultFinder(string? namespaceName, string typeName)
        {
            if (namespaceName is null)
            {
                var fqcn = typeName.Contains(".") ?
                                typeName :
                                $"Avalonia.Controls.{typeName}";


                return AppDomain.CurrentDomain
                                .GetAssemblies()
                                .Select(asm => asm.GetType(fqcn))
                                .OfType<Type>()
                                .FirstOrDefault()
                       ?? throw new ArgumentException($"can't find type: {typeName}");
            }
            throw new ArgumentException($"can't recognise namespace: {namespaceName}");
        }
    }
}
