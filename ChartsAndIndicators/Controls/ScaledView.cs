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

        double barMaxWidth = 80;
        double barMinMargin = 5;
        double textHeight = 22;

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
            var tmpW = (canvasWidth - barValues.Count * barMinMargin) / barValues.Count;
            var barW = Math.Min(tmpW, barMaxWidth);

            int i = 0;
            foreach (var bar in barValues)
            {
                var barH = bar.Value * Scale;

                if (barW <= 0) barW = 2;
                Rectangle rect = new Rectangle();
                rect.Height = Math.Abs(barH);
                rect.Width = barW;
                rect.Stroke = Brushes.Black;
                rect.StrokeThickness = 2;
                rect.Fill = bar.Value >= 0 ? Brushes.DarkGreen : Brushes.DarkRed;

                // seriesW
                var barSeriesW = barValues.Count * (barW + barMinMargin);

                var barX = centerX - (0.5*barSeriesW) + i*(barW + barMinMargin);
                var barY = bar.Value > 0 ? lineZeroY - barH : lineZeroY;

                Canvas.SetLeft(rect, barX );
                Canvas.SetTop(rect, barY );

                var text = new TextBlock();
                text.TextWrapping = TextWrapping.Wrap;
                text.Width = barW + 2*barMinMargin;
                text.TextAlignment = TextAlignment.Center;
                text.Text = $"{bar.Key}\n{ bar.Value.ToString("N1")}";

                Canvas.SetLeft(text, barX - barMinMargin);
                Canvas.SetTop(text, bar.Value <= 0 ? lineZeroY - 2*textHeight : lineZeroY );

                _canvas.Children.Add(rect);
                _canvas.Children.Add(text);

                i++;
            }
        }
    }
}
