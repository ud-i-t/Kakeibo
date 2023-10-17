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
        public int Value => _detales.Sum(d => d.Value);

        public IEnumerable<KeyValuePair<string, int>> Detales => _detales; 
        private List<KeyValuePair<string, int>> _detales = new List<KeyValuePair<string, int>>();

        public Category(string Name, IEnumerable<string> shops) 
        { 
            this.Name = Name;
            _shops = shops;
        }

        public bool Contains(string shopName)
        {
            return _shops.Any(x => shopName.Contains(x));
        }

        public void Add(string shopName, int value) 
        {
            _detales.Add(new KeyValuePair<string, int>(shopName, value));
        }
    }
}
