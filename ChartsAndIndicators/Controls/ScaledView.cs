using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Controls
{
    public class ScaledView
    {
        public double Scale { get; set; }
        public Point Center { get; set; }

        private double ctrlWidth = 0;
        private double ctrlHeight = 0;
        private double ctrlX = 0;
        private double ctrlY = 0;
        double viewMaxH = 0;
        double viewMaxW = 0;
        double lineZeroY = 0;
        double centerX = 0;
        Canvas _canvas;
        double canvasWidth;
        double canvasHeight;

        double barMaxWidth = 40;
        double barMinMargin = 4;

        Dictionary<string, double> barValues = new Dictionary<string, double>();

        public ScaledView(Canvas canvas)
        {
            _canvas = canvas;
        }

        public void Draw(Dictionary<string, double> barValues, int margin = 5)
        {
            ctrlHeight = _canvas.ActualHeight;
            canvasHeight = ctrlHeight- 2*margin;
            ctrlWidth = _canvas.ActualWidth;
            canvasWidth = ctrlWidth- 2*margin;

            var maxH = barValues.Max(x => x.Value);
            var minH = barValues.Min(x => x.Value);
            viewMaxH = minH < 0 ? maxH + Math.Abs( minH) : maxH;

            Scale = canvasHeight / viewMaxH;
            var lineXratio = minH < 0 ? Math.Abs(( maxH + Math.Abs(minH)) / minH) : 1;

            lineZeroY = canvasHeight - (canvasHeight / lineXratio);
            centerX = ctrlWidth / 2;

            _canvas.Children.Clear();

            Line lineZero = new Line();
            lineZero.X1 = margin;
            lineZero.X2 = canvasWidth + margin;
            lineZero.Y1 =  lineZeroY;
            lineZero.Y2 =  lineZeroY;
            lineZero.Stroke = Brushes.Fuchsia;
            lineZero.StrokeThickness = 2;

            _canvas.Children.Add(lineZero);

            Rectangle rect = new Rectangle();
            rect.Height = 4;
            rect.Width = 4;
            rect.Stroke = Brushes.Red;
            rect.StrokeThickness = 2;
            Canvas.SetLeft(rect, 100);
            Canvas.SetTop(rect, 100);

            _canvas.Children.Add(rect);

            DrawScaledBars(barValues);
        }

        public void DrawScaledBars(Dictionary<string, double> barValues)
        {
            int i = 0;
            foreach (var bar in barValues)
            {
                var barH = bar.Value * Scale;
                var barW = Math.Min( barMaxWidth, canvasWidth / (barValues.Count* barMinMargin));

                Rectangle rect = new Rectangle();
                rect.Height = Math.Abs(barH);
                rect.Width = barW;
                rect.Stroke = Brushes.Blue;
                rect.StrokeThickness = 2;

                // seriesW
                var barSeriesW = barValues.Count * (barW + barMinMargin);

                var barX = centerX - (0.5*barSeriesW) + i*(barW + barMinMargin);
                var barY = bar.Value > 0 ? lineZeroY - barH : lineZeroY;

                Canvas.SetLeft(rect, barX );
                Canvas.SetTop(rect, barY );

                _canvas.Children.Add(rect);

                i++;
            }
        }
    }
}
