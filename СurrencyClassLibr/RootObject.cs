using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace СurrencyClassLibr
{
    public class RootObject
    {
        public DateTime Date { get; set; }
        public DateTime PreviousDate { get; set; }
        public string PreviousURL { get; set; }
        public DateTime Timestamp { get; set; }
        public CurrencyType Valute { get; set; }
    }
}
