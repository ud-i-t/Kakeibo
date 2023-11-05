using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kakeibo
{
    internal class Year
    {
        private IDictionary<string, Month> _months;
        private Categories _categories { get; }

        public Year(Categories categories)
        {
            _categories = categories;
            _months = Enumerable
                .Range(1, 12)
                .Select(x => $"2023{x.ToString("00")}.csv")
                .ToDictionary(x => x, x => new Month(categories));
        }

        public void readDetales()
        {
            foreach (var month in _months)
            {
                month.Value.readDetales(month.Key);
            }
        }

        public void summary()
        {
            var utf8Encoding = System.Text.Encoding.UTF8;
            using (StreamWriter outStream = new StreamWriter("summary.csv", false, utf8Encoding))
            {
                outStream.Write(",");
                foreach (var month in _months)
                {
                    outStream.Write(month.Key.Substring(0, 6) + ",");
                }
                outStream.WriteLine();

                foreach (var category in _categories)
                {
                    outStream.Write(category.Name + ",");
                    foreach (var month in _months)
                    {
                        outStream.Write(month.Value.Summary(category) + ",");
                    }
                    outStream.WriteLine();
                }
            }
        }

        public void details()
        {
            foreach (var month in _months)
            {
                month.Value.output(month.Key.Substring(0, 6) + "_detales.txt");
            }
        }
    }
}
