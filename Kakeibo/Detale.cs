using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kakeibo
{
    internal class Detale
    {
        public string CategoryName { get; }
        public int Value { get; }

        public Detale(string categoryName, int value)
        {
            CategoryName = categoryName;
            Value = value;
        }
    }
}
