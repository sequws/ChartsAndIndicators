using Controls.Common;
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
    public class BarChartDrawer : ChartDrawer
    {
        Canvas canvas;

        double barMaxWidth = 80;
        double barMinMargin = 5;
        double textHeight = 10;

        public BarChartDrawer(Canvas canvas) : base(canvas)
        {
            this.canvas = canvas;
        }

        public void Draw(Dictionary<string, double> barValues, int margin = 10)
        {
            if (barValues.Count == 0) return;

            var barMaxH = barValues.Max(x => x.Value);
            var barMinH = barValues.Min(x => x.Value);

            // from base-
            CalculateScale(barMinH, barMaxH, barValues.Count, 10);
            Draw();

            DrawBars(barValues, margin);
        }

        private void DrawBars(Dictionary<string, double> barValues, int margin)
        {
            var tmpW = (ScaleCalculator.CanvasWidth - barValues.Count * barMinMargin - 2*margin) / barValues.Count;
            var barW = Math.Min(tmpW, barMaxWidth);

            int i = 0;
            foreach (var bar in barValues)
            {
                var barH = bar.Value * ScaleCalculator.Scale;

                if (barW <= 0) barW = 2;
                Rectangle rect = new Rectangle();
                rect.Height = Math.Abs(barH);
                rect.Width = barW;
                rect.Stroke = Brushes.Black;
                rect.StrokeThickness = 2;
                rect.Fill = bar.Value >= 0 ? Brushes.DarkGreen : Brushes.DarkRed;

                // seriesW
                var barSeriesW = barValues.Count * (barW + barMinMargin) + barMinMargin;

                var barX = ScaleCalculator.CanvasMargin + barMinMargin + ScaleCalculator.CenterX + i * (barW + barMinMargin) - (0.5 * barSeriesW);
                var barY = bar.Value > 0 ? ScaleCalculator.LineZeroY - barH : ScaleCalculator.LineZeroY;

                Canvas.SetLeft(rect, barX);
                Canvas.SetTop(rect, barY);

                var text = new TextBlock();
                text.TextWrapping = TextWrapping.Wrap;
                text.Width = barW + 2 * barMinMargin;
                text.TextAlignment = TextAlignment.Center;
                text.Text = $"{bar.Key}\n{ bar.Value.ToString("N1")}";
                text.FontSize = textHeight;

                Canvas.SetLeft(text, barX - barMinMargin);
                Canvas.SetTop(text, bar.Value <= 0 ? ScaleCalculator.LineZeroY - 2 * (textHeight + 4) : ScaleCalculator.LineZeroY);
                text.Visibility = text.Width < 4 * 8 ? Visibility.Hidden : Visibility.Visible;

                canvas.Children.Add(rect);
                canvas.Children.Add(text);

                i++;
            }
        }
    }
}
