using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Kakeibo
{
    internal class Categories : IEnumerable<Category>
    {
        private IEnumerable<Category> _categories;
        public Category _uncategorized = new Category("その他", new List<string>());

        public Categories(string filePath)
        {
            _categories = readShops(filePath).Union(new List<Category>() { _uncategorized });
        }

        public Category GetCategory(string shopName)
        {
            var category = _categories.FirstOrDefault(x => x.Contains(shopName));
            if (category == null)
            {
                return _uncategorized;
            }
            return category;
        }

        private IEnumerable<Category> readShops(string filePath)
        {
            IEnumerable<string> lines = File.ReadLines(filePath);
            List<Category> categories = new List<Category>();

            while (lines.Count() > 0)
            {
                string category = lines.First();
                category = category.Substring(1, category.Length - 2);

                lines = lines.Skip(1).ToList();
                IEnumerable<string> shops = lines.TakeWhile(x => !x.StartsWith('['));

                categories.Add(new Category(category, shops));
                lines = lines.SkipWhile(x => !x.StartsWith('['));
            }

            return categories;
        }

        public IEnumerator<Category> GetEnumerator()
        {
            return _categories.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_categories).GetEnumerator();
        }
    }
}
