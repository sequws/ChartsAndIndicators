using Controls.Common;
using Controls.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Controls
{
    public class OhlcChartDrawer : ScalableChartDrawer
    {
        Canvas canvas;
        List<Ohlc> ohlcCandles = new List<Ohlc>();

        public double ChartMargin { get; set; } = 70;
        public double CandleMargin { get; set; } = 2;
        public double CandleWidth { get; set; } = 20;

        public OhlcChartDrawer(Canvas canvas, int decimalPlaces = 5) : base(canvas)
        {
            this.canvas = canvas;
            DecimalPlaces = decimalPlaces;
        }

        public void Draw( List<Ohlc> ohlcCandles)
        {
            if (ohlcCandles.Count == 0) return;
            if (canvas.ActualWidth == 0 || canvas.ActualHeight == 0) return;

            int dataLength = ohlcCandles.Count;
            var max = ohlcCandles.Max(x => x.High);
            var min = ohlcCandles.Min(x => x.Low);

            CalculateInitialScale(canvas, min, max, dataLength);  // Cant properly calculate without Zero on chart!
            //Draw();
            DrawCandles(ohlcCandles);
        }

        private void DrawCandles(List<Ohlc> ohlcCandles)
        {
            var candleSeriesWidth = ohlcCandles.Count * (CandleMargin * 2 + CandleWidth);

            int i = 0;
            foreach (var ohlc in ohlcCandles)
            {
                var height = Math.Abs(ohlc.Open - ohlc.Close) * Calculator.InitialScale;
                if (height == 0) height = 2;

                Rectangle rect = new Rectangle();
                rect.Width = CandleWidth;
                rect.Height = height;
                rect.Stroke = Brushes.Black;
                rect.StrokeThickness = 2;
                rect.Fill = ohlc.Open <= ohlc.Close ? Brushes.DarkGreen : Brushes.DarkRed;

                // center of candle
                var ohlcCenter = AxisYPosX - candleSeriesWidth - ChartMargin + (CandleMargin*2 + CandleWidth) * i;
                var left = ohlcCenter - CandleMargin - 0.5 * CandleWidth;
                Canvas.SetLeft(rect,left);

                // if the candle is off the chart
                if (left <= 0)
                {
                    i++;
                    continue;
                }

                var top = ohlc.Close >= ohlc.Open ? ohlc.Close : ohlc.Open;
                Canvas.SetTop(rect, Calculator.CalcY( top));

                Line line = new Line();
                line.X1 = ohlcCenter - CandleMargin;
                line.X2 = line.X1;
                line.Stroke = ohlc.Open <= ohlc.Close ? Brushes.DarkGreen : Brushes.DarkRed;
                line.Y1 = Calculator.CalcY(ohlc.Low);
                line.Y2 = Calculator.CalcY(ohlc.High);

                canvas.Children.Add(line);
                canvas.Children.Add(rect);
                i++;
            }
        }
    }
}
