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

            double max = 100;
            double min = 0;
            int dataLength = ohlcCandles.Count;

            //foreach (var lineData in linesData)
            //{
            //    var MaximumValue = lineData.Value.Max();
            //    var MinimumValue = lineData.Value.Min();

            //    max = MaximumValue > max ? MaximumValue : max;
            //    min = MinimumValue < min ? MinimumValue : min;
            //    if (lineData.Value.Count > dataLength) dataLength = lineData.Value.Count;
            //}

            CalculateScale(min, max, dataLength, 10);
            Draw();
            DrawCandles(ohlcCandles);
        }

        private void DrawCandles(List<Ohlc> ohlcCandles)
        {
            
        }
    }
}
