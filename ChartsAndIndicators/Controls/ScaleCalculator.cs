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
    /// <summary>
    /// Scale Calculator - calculates scale, for given min/max. 
    /// Draws the base of the graph, and the X and Y axes along with the scale.
    /// </summary>
    public class ScaleCalculator
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
        double _canvasMargin;
        double canvasWidth;
        double canvasHeight;

        double minH = 0;
        double maxH = 0;

        int yAxisSteps = 0;
        int yLastStep = 0;

        int stepHeightPix = 50;

        public int MaxStepsOnYAxis { get; set; } = 20;

        public ScaleCalculator(Canvas canvas, int margin = 0)
        {
            _canvas = canvas;
            _canvasMargin = margin;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="min">Min value from all data</param>
        /// <param name="max">Max value from all data</param>
        /// <param name="dataLength">Numbers of points, bars etc.</param>
        public void CalculateScale(double min, double max, int dataLength)
        {
            if (dataLength == 0) return;

            ctrlHeight = _canvas.ActualHeight;
            canvasHeight = ctrlHeight - 2 * _canvasMargin;
            ctrlWidth = _canvas.ActualWidth;
            canvasWidth = ctrlWidth - 2 * _canvasMargin;

            var barMaxH = Math.Max(max, FixedMaxH);
            var barMinH = Math.Min(min, FixedMinH);
            stepHeightPix = CalcStepHeight(barMinH, barMaxH, MaxStepsOnYAxis);

            maxH = RoundToFirstPlus(barMaxH, stepHeightPix);
            minH = RoundToFirstMinus(barMinH, stepHeightPix);
            yAxisSteps = (int)((maxH + Math.Abs(minH)) / stepHeightPix) + 1;

            yLastStep = (int)minH / stepHeightPix;

            if (maxH == 0) maxH = 1;
            if (minH == 0) minH = -1;

            viewFullHeight = minH < 0 ? maxH + Math.Abs(minH) : maxH;
            Scale = canvasHeight / viewFullHeight;

            var lineXratio = minH < 0 ? Math.Abs((maxH + Math.Abs(minH)) / minH) : 1;

            lineZeroY = lineXratio > 1 ? canvasHeight - (canvasHeight / lineXratio) :
                minH < 0 ? 0 : viewFullHeight * Scale;
            centerX = ctrlWidth / 2;

            DrawLineZero();
            DrawAxisY();
        }

        private void DrawLineZero()
        {
            _canvas.Children.Clear();

            Line lineZero = new Line();
            lineZero.X1 = _canvasMargin;
            lineZero.X2 = canvasWidth + _canvasMargin;
            lineZero.Y1 = lineZeroY;
            lineZero.Y2 = lineZeroY;
            lineZero.Stroke = Brushes.Fuchsia;
            lineZero.StrokeThickness = 2;

            _canvas.Children.Add(lineZero);
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
            var stepH = viewFullHeight * Scale / (steps - 1);

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
                step.X2 = canvasWidth;
                step.Y1 = stepBottomY - i * stepH;
                step.Y2 = step.Y1;

                _canvas.Children.Add(step);
            }
        }


        #region helpers metod

        public static int CalcStepHeight(double min, double max, int maxSteps = 10)
        {
            int stepHeigh = 0;

            var cmax = RoundToFirstPlus(max);
            var cmin = RoundToFirstMinus(min);

            stepHeigh = RoundToFirstPlus((cmax + Math.Abs(cmin)) / maxSteps);

            return stepHeigh;
        }

        /// <summary>
        /// Returns first narest rounded to ten value
        /// </summary>
        public static int RoundToFirstPlus(double barMax, int roundedTo = 10)
        {
            var maxTenth = (int)(barMax / roundedTo) * roundedTo;
            if (barMax % roundedTo != 0) maxTenth += roundedTo;

            return maxTenth;
        }

        /// <summary>
        /// Returns minimal first rounded to ten value
        /// </summary>
        public static int RoundToFirstMinus(double barMin, int roundedTo = 10)
        {
            var minTenth = (int)(barMin / roundedTo) * roundedTo;
            if (barMin % roundedTo != 0) minTenth -= roundedTo;

            return minTenth;
        }

        #endregion
    }
}
