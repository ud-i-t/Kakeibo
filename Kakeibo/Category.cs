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

        public IEnumerable<Detale> Detales => _detales; 
        private List<Detale> _detales = new List<Detale>();

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
            _detales.Add(new Detale(shopName, value));
        }
    }
}
