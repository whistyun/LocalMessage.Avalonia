using DemoAvaloniaMainApp;
using LocalMessage.Avalonia;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace TestForAvalonia
{
    public class UnitTest2
    {
        public void LocPropTestApp()
        {
            MessageService.Instance.Culture = new CultureInfo("en-US");

            Assert.AreEqual("App Props Default 1", MessageGetter.MessageProp1("Message1"));
            Assert.AreEqual("App Props Default 2", MessageGetter.MessageProp2("Message2"));
            Assert.AreEqual("App Props Default 3", MessageGetter.MessageProp3("Message3"));

            MessageService.Instance.Culture = new CultureInfo("ja-JP");

            Assert.AreEqual("App Props ja-JP 1", MessageGetter.MessageProp1("Message1"));
            Assert.AreEqual("App Props ja-JP 2", MessageGetter.MessageProp2("Message2"));
            Assert.AreEqual("App Props ja-JP 3", MessageGetter.MessageProp3("Message3"));
        }

        public void LocAnothTestApp()
        {
            MessageService.Instance.Culture = new CultureInfo("en-US");

            Assert.AreEqual("App Another Default 1", MessageGetter.MessageAnoth1("Message1"));
            Assert.AreEqual("App Another Default 2", MessageGetter.MessageAnoth2("Message2"));
            Assert.AreEqual("App Another Default 3", MessageGetter.MessageAnoth3("Message3"));

            MessageService.Instance.Culture = new CultureInfo("ja-JP");

            Assert.AreEqual("App Another ja-JP 1", MessageGetter.MessageAnoth1("Message1"));
            Assert.AreEqual("App Another ja-JP 2", MessageGetter.MessageAnoth2("Message2"));
            Assert.AreEqual("App Another ja-JP 3", MessageGetter.MessageAnoth3("Message3"));
        }

        public void LocPropTestLib()
        {
            MessageService.Instance.Culture = new CultureInfo("en-US");

            Assert.AreEqual("Lib Props Default 1", MessageGetter2.MessageProp1("Message1"));
            Assert.AreEqual("Lib Props Default 2", MessageGetter2.MessageProp2("Message2"));
            Assert.AreEqual("Lib Props Default 3", MessageGetter2.MessageProp3("Message3"));

            MessageService.Instance.Culture = new CultureInfo("ja-JP");

            Assert.AreEqual("Lib Props ja-JP 1", MessageGetter2.MessageProp1("Message1"));
            Assert.AreEqual("Lib Props ja-JP 2", MessageGetter2.MessageProp2("Message2"));
            Assert.AreEqual("Lib Props ja-JP 3", MessageGetter2.MessageProp3("Message3"));
        }

        public void LocAnothTestLib()
        {
            MessageService.Instance.Culture = new CultureInfo("en-US");

            Assert.AreEqual("Lib Another Default 1", MessageGetter2.MessageAnoth1("Message1"));
            Assert.AreEqual("Lib Another Default 2", MessageGetter2.MessageAnoth2("Message2"));
            Assert.AreEqual("Lib Another Default 3", MessageGetter2.MessageAnoth3("Message3"));

            MessageService.Instance.Culture = new CultureInfo("ja-JP");

            Assert.AreEqual("Lib Another ja-JP 1", MessageGetter2.MessageAnoth1("Message1"));
            Assert.AreEqual("Lib Another ja-JP 2", MessageGetter2.MessageAnoth2("Message2"));
            Assert.AreEqual("Lib Another ja-JP 3", MessageGetter2.MessageAnoth3("Message3"));
        }
    }
}
