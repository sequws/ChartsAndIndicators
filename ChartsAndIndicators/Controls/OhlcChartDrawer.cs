using Controls.Common;
using Controls.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Controls
{
    public class OhlcChartDrawer : ChartDrawer
    {
        Canvas canvas;
        List<Ohlc> ohlcCandles = new List<Ohlc>();

        public OhlcChartDrawer(Canvas canvas) : base(canvas)
        {
            this.canvas = canvas;
        }

        public void Draw( List<Ohlc> ohlcCandles)
        {
            if (ohlcCandles.Count == 0) return;

            int dataLength = ohlcCandles.Count;
            var max = ohlcCandles.Max(x => x.High);
            var min = ohlcCandles.Min(x => x.Low);

            CalculateScale(min, max, dataLength, 10);  // Cant properly calculate without Zero on chart!
            Draw();
            DrawCandles(ohlcCandles);
        }

        private void DrawCandles(List<Ohlc> ohlcCandles)
        {
            
        }
    }
}
