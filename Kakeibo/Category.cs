using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kakeibo
{
    internal class Category
    {
        public string Name { get; }
        private IEnumerable<string> _shops;

        public Category(string Name, IEnumerable<string> shops) 
        { 
            this.Name = Name;
            _shops = shops;
        }

        public bool Contains(string shopName)
        {
            return _shops.Any(x => shopName.Contains(x));
        }
    }
}
