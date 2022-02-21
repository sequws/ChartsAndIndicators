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
        public double FixedMinH { get; set; } = -1;
        public double FixedMaxH { get; set; } = 1;

        private double ctrlWidth = 0;
        private double ctrlHeight = 0;
        double viewFullHeight = 0;
        double lineZeroY = 0;
        double centerX = 0;
        Canvas _canvas;
        double canvasWidth;
        double canvasHeight;

        double barMaxWidth = 80;
        double barMinMargin = 5;
        double textHeight = 10;

        double minH = 0;
        double maxH = 0;

        public ScaledView(Canvas canvas)
        {
            _canvas = canvas;
        }

        public void Draw(Dictionary<string, double> barValues, int margin = 0)
        {
            if (barValues.Count == 0) return;

            ctrlHeight = _canvas.ActualHeight;
            canvasHeight = ctrlHeight - 2*margin;
            ctrlWidth = _canvas.ActualWidth;
            canvasWidth = ctrlWidth - 2*margin;

            maxH = Math.Max( barValues.Max(x => x.Value), FixedMaxH);
            minH = Math.Min( barValues.Min(x => x.Value), FixedMinH);

            if (maxH == 0) maxH = 1;
            if (minH == 0) minH = -1;

            viewFullHeight = minH < 0 ? maxH + Math.Abs( minH) : maxH;

            Scale = canvasHeight / viewFullHeight;
            var lineXratio = minH < 0 ? Math.Abs(( maxH + Math.Abs(minH)) / minH) : 1;

            lineZeroY = lineXratio > 1 ? canvasHeight - (canvasHeight / lineXratio) : 
                minH < 0 ? 0 : viewFullHeight * Scale;
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
            DrawScaledBars(barValues);
            DrawAxisY();
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
                text.FontSize = textHeight;

                Canvas.SetLeft(text, barX - barMinMargin);
                Canvas.SetTop(text, bar.Value <= 0 ? lineZeroY - 2*(textHeight+4) : lineZeroY );
                text.Visibility = text.Width < 4 * 8 ? Visibility.Hidden : Visibility.Visible;

                _canvas.Children.Add(rect);
                _canvas.Children.Add(text);

                i++;
            }
        }

        private void DrawAxisY()
        {
            Line axisY = new Line();
            axisY.Stroke = Brushes.Black;
            axisY.StrokeThickness = 1;

            axisY.X1 = 0;
            axisY.X2 = 0;
            axisY.Y1 = lineZeroY -  minH * Scale;
            axisY.Y2 = lineZeroY - maxH * Scale;

            // top and bottom line
            Line stepTop = new Line();
            stepTop.Stroke = Brushes.Black;
            stepTop.StrokeThickness = 1;
            stepTop.X1 = 0;
            stepTop.X2 = 10;
            stepTop.Y1 = lineZeroY - maxH * Scale;
            stepTop.Y2 = stepTop.Y1;

            Line stepBottom = new Line();
            stepBottom.Stroke = Brushes.Black;
            stepBottom.StrokeThickness = 1;
            stepBottom.X1 = 0;
            stepBottom.X2 = 10;
            stepBottom.Y1 = lineZeroY - minH * Scale;
            stepBottom.Y2 = stepBottom.Y1;

            _canvas.Children.Add(axisY);
            _canvas.Children.Add(stepTop);
            _canvas.Children.Add(stepBottom);

            var steps = 10;
            var stepH = (maxH - minH) * Scale / steps;
            var stepBottomY = lineZeroY - minH * Scale;

            for (int i = 1; i < steps; i++)
            {
                Line step = new Line();
                step.Stroke = Brushes.Black;
                step.StrokeThickness = 1;
                step.X1 = 0;
                step.X2 = 10;
                step.Y1 = stepBottomY - i*stepH;
                step.Y2 = step.Y1;

                _canvas.Children.Add(step);
            }

            var text = new TextBlock();
            text.TextWrapping = TextWrapping.Wrap;
            text.Text = $"{ maxH.ToString("N1")}";
            text.FontSize = textHeight;
            Canvas.SetLeft(text, stepTop.X2);
            Canvas.SetTop(text, stepTop.Y2 - textHeight /2);

            var text2 = new TextBlock();
            text2.TextWrapping = TextWrapping.Wrap;
            text2.Text = $"{ minH.ToString("N1")}";
            text2.FontSize = textHeight;
            Canvas.SetLeft(text2, stepBottom.X2);
            Canvas.SetTop(text2, stepBottom.Y2 - textHeight /2);

            _canvas.Children.Add(text);
            _canvas.Children.Add(text2);

        }
    }
}
