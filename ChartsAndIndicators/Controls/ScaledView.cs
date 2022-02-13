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
        double canvasW;
        double canvasH;

        double barMaxWidth = 40;

        Dictionary<string, double> barValues = new Dictionary<string, double>();

        public ScaledView(Canvas canvas)
        {
            _canvas = canvas;
        }

        public void Draw(Dictionary<string, double> barValues, int margin = 5)
        {
            ctrlHeight = _canvas.ActualHeight;
            canvasH = ctrlHeight- 2*margin;
            ctrlWidth = _canvas.ActualWidth;
            canvasW = ctrlWidth- 2*margin;

            var maxH = barValues.Max(x => x.Value);
            var minH = barValues.Min(x => x.Value);
            viewMaxH = minH < 0 ? maxH + Math.Abs( minH) : maxH;

            Scale = canvasH / viewMaxH;
            var lineXratio = minH < 0 ? maxH / minH : 1;

            lineZeroY = canvasH / lineXratio;

            _canvas.Children.Clear();

            Line lineZero = new Line();
            lineZero.X1 = margin;
            lineZero.X2 = canvasW + margin;
            lineZero.Y1 = lineZeroY;
            lineZero.Y2 = lineZeroY;
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
        }

        public void DrawScaledBars(Dictionary<string, double> barValues)
        {
            int i = 0;
            foreach (var bar in barValues)
            {
                var barH = bar.Value * Scale;
                var barW = barMaxWidth;


                Rectangle rect = new Rectangle();
                rect.Height = Math.Abs(barH);
                rect.Width = barW;
                rect.Stroke = Brushes.Blue;
                rect.StrokeThickness = 2;

                //Canvas.SetLeft(rect, barX + i * 50);
                //Canvas.SetTop(rect, barY - rect.Height);

                i++;
            }
        }
    }
}
