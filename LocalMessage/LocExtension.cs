using System;
using LocalMessage.Utils;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Linq;
using System.Resources;
using System.Collections.Concurrent;

#if IS_AVALONIA
using Avalonia.Data;
using Avalonia.Markup.Xaml;

namespace LocalMessage.Avalonia
#endif

#if IS_WPF
using System.Windows.Data;
using System.Windows.Markup;
using System.Xaml;

namespace LocalMessage.WPF
#endif
{
    public class LocExtension : MarkupExtension
    {
        private static Regex s_packPtn = new("^/([^/;]+)(;[^/;]+)*?;component");
        private static ConcurrentDictionary<ResourceKey, ResourceManager> s_cache = new();

        private BindingCreator Creator { get; }

        public LocExtension(Binding messageBinding)
        {
            Creator = new BindingCreator2(messageBinding);
        }

        public LocExtension(string message)
        {
            Creator = new BindingCreator1(message);
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            string? asmNm = null;
            string? rscNm = null;

            var rootObj = serviceProvider.Find<IRootObjectProvider>().RootObject;
            asmNm = PropUtils.GetAttachedProperty(rootObj, MessageService.AssemblyNameProperty);
            rscNm = PropUtils.GetAttachedProperty(rootObj, MessageService.ResourceNameProperty);

            if (asmNm is null)
            {
                var baseUri = serviceProvider.Find<IUriContext>().BaseUri;

                // avares://asmname/resourcename 
                // or
                // pack://???,,,/asmname:component/???
                // or
                // pack://???,,,/???
                asmNm = baseUri?.Scheme switch
                {
                    "avares" => SolveAssemblyNameFromAvares(baseUri),
                    "pack" => SolveAssemblyNameFromPack(baseUri),
                    _ => null
                };
            }

            if (asmNm is null)
            {
                var asm = Assembly.GetEntryAssembly()
                    ?? throw new InvalidOperationException($"Failed to load Entry Assembly");

                asmNm = asm.GetName().Name
                    ?? throw new InvalidOperationException($"Failed to load Entry Assembly");
            }

            if (rscNm is null)
            {
                rscNm = $"{asmNm }.Properties.Resource";
            }

            var manager = s_cache.GetOrAdd(new ResourceKey(asmNm, rscNm), Load);

            return Creator.Create(manager);
        }

        private static string SolveAssemblyNameFromAvares(Uri uri) => uri.Host;

        private static string? SolveAssemblyNameFromPack(Uri uri)
        {
            var path = uri.LocalPath;

            var match = s_packPtn.Match(path);
            return match.Success ? match.Groups[1].Value : null;
        }

        private static ResourceManager Load(ResourceKey key)
        {
            var asmNm = key.Assembly;
            var rscNm = key.Resource;

            var asm = AppDomain.CurrentDomain
                               .GetAssemblies()
                               .FirstOrDefault(asm => Eq(asmNm, asm.GetName().Name));

            if (asm is null)
            {
                throw new InvalidOperationException($"Failed to load Assembly '{asmNm}'");
            }

            var rscTp = asm.GetType(rscNm);
            if (rscTp is null)
            {
                throw new InvalidOperationException($"Failed to load '{rscNm}' type in '{asmNm}' assembly");
            }

            static bool Eq(string t1, string? t2)
                => t1.Equals(t2, StringComparison.InvariantCultureIgnoreCase);

            return new ResourceManager(rscNm, asm);
        }
    }
}
