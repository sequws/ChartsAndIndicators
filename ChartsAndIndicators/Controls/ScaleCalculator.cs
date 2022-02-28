using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Controls
{
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

        public ScaleCalculator(Canvas canvas)
        {
            _canvas = canvas;
        }

        public void CalculateScale()
        {

        }
    }
}
