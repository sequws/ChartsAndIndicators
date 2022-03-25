using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Models
{
    public class Ohlc
    {
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public double Volume { get; set; }

        public Ohlc(double open, double high, double low, double close)
        {
            Open = open;
            High = high;
            Low = low;
            Close = close;
        }

        public Ohlc()
        {

        }
    }
}
