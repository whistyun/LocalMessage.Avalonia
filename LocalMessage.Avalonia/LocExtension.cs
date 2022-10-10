using Avalonia.Data;
using Avalonia.Markup.Xaml;
using System;
using System.Reflection;
using System.Linq;
using System.Resources;
using System.Collections.Concurrent;
using LocalMessage.Avalonia.Utils;

namespace LocalMessage.Avalonia
{
    public class LocExtension : MarkupExtension
    {
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
            asmNm = PropUtil.GetAttachedProperty(rootObj, MessageService.AssemblyNameProperty);
            rscNm = PropUtil.GetAttachedProperty(rootObj, MessageService.ResourceNameProperty);

            if (asmNm is null)
            {
                var baseUri = serviceProvider.Find<IUriContext>().BaseUri;

                // avares://asmname/resourcename 
                asmNm = baseUri?.Scheme == "avares" ? baseUri.Host : null;
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
                rscNm = $"{asmNm}.Properties.Resource";
            }

            return Creator.Create(MessageService.GetResource(new ResourceKey(asmNm, rscNm)));
        }
    }
}
