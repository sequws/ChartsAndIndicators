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

        int yAxisSteps = 0;
        int yLastStep = 0;

        int stepHeightPix = 50;

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

            var barMaxH = Math.Max(barValues.Max(x => x.Value), FixedMaxH);
            var barMinH = Math.Min(barValues.Min(x => x.Value), FixedMinH);

            stepHeightPix = ScaleCalculator.CalcStepHeight(barMinH, barMaxH,20);

            maxH = ScaleCalculator.RoundToFirstPlus(barMaxH, stepHeightPix); 
            minH = ScaleCalculator.RoundToFirstMinus(barMinH, stepHeightPix); 
            yAxisSteps = (int)((maxH + Math.Abs( minH)) / stepHeightPix) +1;

            yLastStep = (int)minH / stepHeightPix;

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
            DrawAxisY();
            DrawScaledBars(barValues);
            DrawAxisYText();
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
            axisY.X1 = 0;
            axisY.X2 = 0;
            axisY.Y1 = lineZeroY - minH * Scale;
            axisY.Y2 = lineZeroY - maxH * Scale;
            axisY.Stroke = Brushes.Black;
            axisY.StrokeThickness = 1;            
            _canvas.Children.Add(axisY);

            var steps = yAxisSteps;
            var stepH = viewFullHeight * Scale / (steps-1);

            var stepBottomY = lineZeroY - yLastStep * stepH;

            for (int i = 0; i < steps; i++)
            {
                Line step = new Line();
                step.Stroke = Brushes.LightGray;
                step.StrokeThickness = 1;
                var dashArray = new DoubleCollection();
                dashArray.Add(2);
                dashArray.Add(2);
                step.StrokeDashArray = dashArray;
                step.X1 = 0;
                step.X2 = canvasWidth;// 10;
                step.Y1 = stepBottomY - i * stepH;
                step.Y2 = step.Y1;

                _canvas.Children.Add(step);
            }
        }

        private void DrawAxisYText(int marginLeft = 10, int marginTop = 14)
        {
            var text = new TextBlock();
            text.TextWrapping = TextWrapping.Wrap;
            text.Text = $"{ maxH.ToString("N1")}";

            text.FontSize = textHeight;
            Canvas.SetLeft(text, marginLeft);
            Canvas.SetTop(text, lineZeroY - maxH * Scale - marginTop);

            var text2 = new TextBlock();
            text2.TextWrapping = TextWrapping.Wrap;
            text2.Text = $"{ minH.ToString("N1")}";

            text2.FontSize = textHeight;
            Canvas.SetLeft(text2, marginLeft);
            Canvas.SetTop(text2, lineZeroY - minH * Scale - marginTop);

            var text0 = new TextBlock();
            text0.Text = $"0";
            text0.FontSize = textHeight;
            Canvas.SetLeft(text0, marginLeft);
            Canvas.SetTop(text0, lineZeroY - marginTop);

            _canvas.Children.Add(text0);
            _canvas.Children.Add(text);
            _canvas.Children.Add(text2);
        }

        /// <summary>
        /// Returns first narest rounded to ten value
        /// </summary>
        private double GetYAxisMax(double barMax)
        {
            var maxTenth = (int)(barMax / 10) * 10;
            if (barMax % 10 != 0) maxTenth += 10;

            return maxTenth;
        }

        /// <summary>
        /// Returns minimal first rounded to ten value
        /// </summary>
        private double GetYAxisMin(double barMin)
        {
            var minTenth = (int)(barMin / 10) * 10;
            if (barMin % 10 != 0) minTenth -= 10;

            return minTenth;
        }
    }
}
