using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kakeibo
{
    internal class Detale
    {
        public Category Category { get; }
        public string ShopName { get; }
        public int Value { get; }

        public Detale(Category category, string shopName, int value)
        {
            Category = category;
            ShopName = shopName;
            Value = value;
        }
    }
}
