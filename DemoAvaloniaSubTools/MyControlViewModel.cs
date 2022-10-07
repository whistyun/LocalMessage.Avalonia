using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoAvaloniaSubTools
{
    class MyControlViewModel
    {
        public List<Item> Children { get; } = new()
        {
            new Item("Message1"),
            new Item("Message2"),
            new Item("Message3")
        };
    }

    class Item
    {
        public string Value { get; }

        public Item(string val) { Value = val; }
    }
}
