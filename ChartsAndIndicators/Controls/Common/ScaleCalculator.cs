using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Controls.Common
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
        double _lineZeroY = 0;
        double centerX = 0;
        double dataLength = 0;

        Canvas _canvas;
        double _canvasMargin = 10;

        double canvasWidth;
        double canvasHeight;

        double _minH = 0;
        double _maxH = 0;

        int yAxisSteps = 0;
        int yLastStep = 0;
        double stepHeight = 0;
        double stepBottomY = 0;
        double stepSizeVal = 0;

        int stepHeightPix = 50;

        public int MaxStepsOnYAxis { get; set; } = 20;
        public int TextHeight { get; set; } = 10;

        public double LineZeroY => _lineZeroY;
        public double CenterX => centerX;
        public double MinH => _minH;
        public double MaxH => _maxH;
        public int LastStepY => yLastStep;
        public int AxisYStepsNum => yAxisSteps;
        public double StepHeight => stepHeight;
        public double StepBottomY => stepBottomY;
        public double StepSizeVal => stepSizeVal;   // size of step in scale
        public double CanvasWidth => canvasWidth;
        public double CanvasHeight => canvasHeight;
        public double CanvasMargin => _canvasMargin;

        public double ViewFullHeight => viewFullHeight;
        public double DataLength => dataLength;

        public ScaleCalculator(Canvas canvas)
        {
            _canvas = canvas;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="min">Min value from all data</param>
        /// <param name="max">Max value from all data</param>
        /// <param name="dataLength">Numbers of points, bars etc.</param>
        public void CalculateScale(double min, double max, int dataLength, int margin = 0)
        {
            if (dataLength == 0) return;
            this.dataLength = dataLength;

            _canvasMargin = margin;
            ctrlHeight = _canvas.ActualHeight;
            canvasHeight = ctrlHeight - 2 * _canvasMargin;
            ctrlWidth = _canvas.ActualWidth;
            canvasWidth = ctrlWidth - 2 * _canvasMargin;
            centerX = canvasWidth / 2;

            var barMaxH = Math.Max(max, FixedMaxH);
            var barMinH = Math.Min(min, FixedMinH);
            stepHeightPix = CalculatorHelpers.CalcStepHeight(barMinH, barMaxH, MaxStepsOnYAxis);

            _maxH = CalculatorHelpers.RoundToFirstPlus(barMaxH, stepHeightPix);
            _minH = CalculatorHelpers.RoundToFirstMinus(barMinH, stepHeightPix);
            yAxisSteps = (int)((_maxH + Math.Abs(_minH)) / stepHeightPix) + 1;

            yLastStep = (int)_minH / stepHeightPix;

            if (_maxH == 0) _maxH = 1;
            if (_minH == 0) _minH = -1;

            viewFullHeight = _minH < 0 ? _maxH + Math.Abs(_minH) : _maxH;
            Scale = canvasHeight / viewFullHeight;

            var lineXratio = _minH < 0 ? Math.Abs((_maxH + Math.Abs(_minH)) / _minH) : 1;

            _lineZeroY = lineXratio > 1 ? canvasHeight - (canvasHeight / lineXratio) :
                _minH < 0 ? 0 : viewFullHeight * Scale;
            _lineZeroY += margin;

            stepHeight = ViewFullHeight * Scale / (yAxisSteps - 1);
            stepBottomY = LineZeroY - LastStepY * stepHeight;

            stepSizeVal = (Math.Abs(MinH) + MaxH) / (AxisYStepsNum - 1);
        }


    }
}
