using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kakeibo
{
    internal class Month
    {
        public IEnumerable<Detale> Detales => _detales;
        private List<Detale> _detales = new List<Detale>();

        private Categories _categories;

        public Month(Categories categories)
        {
            _categories = categories;
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
                var category = _categories.GetCategory(shopName);
                Add(category, shopName, int.Parse(csvLine[5]));
            }
        }

        public void output()
        {
            using (StreamWriter outStream = new StreamWriter("out.txt"))
            {
                // 概要出力
                outStream.WriteLine("【概要】");
                foreach (var category in _categories)
                {
                    outStream.WriteLine($"{category.Name}, {_detales.Where(x => x.Category == category ).Sum(x => x.Value)}");
                }

                // 明細出力

                outStream.WriteLine("");
                outStream.WriteLine("【明細】");
                foreach (var category in _categories)
                {
                    outStream.WriteLine($"{category.Name}:");
                    foreach (var detale in _detales.Where(x => x.Category == category))
                    {
                        outStream.WriteLine($"{detale.ShopName}, {detale.Value}");
                    }
                    outStream.WriteLine("");
                }
            }
        }

        public void Add(Category category, string shopName, int value)
        {
            _detales.Add(new Detale(category, shopName, value));
        }
    }
}
