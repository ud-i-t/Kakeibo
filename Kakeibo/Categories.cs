using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kakeibo
{
    internal class Categories
    {
        private IEnumerable<Category> _categories;
        private Category _uncategorized = new Category("分類不能", new List<string>());

        public Categories(string filePath)
        {
            _categories = readShops(filePath).Union(new List<Category>() { _uncategorized });
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

        public void readDetales(string filePath) 
        {
            // ファイル読込
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var eis = System.Text.Encoding.GetEncodings();

            // 1行目はヘッダなので読み飛ばす
            IEnumerable<string> lines = File.ReadLines(filePath, System.Text.Encoding.GetEncoding("shift_jis")).Skip(1);

            // 末尾はカット
            lines = lines.Take(lines.Count() - 1).ToList();

            foreach (var line in lines)
            {
                var csvLine = line.Split(',');
                var shopName = csvLine[1];
                var category = _categories.FirstOrDefault(x => x.Contains(shopName));
                if (category == null)
                {
                    _uncategorized.Add(shopName, int.Parse(csvLine[5]));
                    continue;
                }
                category.Add(shopName, int.Parse(csvLine[5]));
            }
        }

        public void output()
        {
            // 概要出力
            Console.WriteLine("【概要】");
            foreach (var category in _categories)
            {
                Console.WriteLine($"{category.Name}, {category.Value}");
            }

            // 明細出力

            Console.WriteLine("");
            Console.WriteLine("【明細】");
            foreach (var category in _categories)
            {
                Console.WriteLine($"{category.Name}:");
                foreach (var detale in category.Detales)
                {
                    Console.WriteLine($"{detale.Key}, {detale.Value}");
                }
                Console.WriteLine("");
            }
        }
    }
}
