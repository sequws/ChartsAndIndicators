using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Controls.Common
{
    /// <summary>
    /// Allow draw chart which center can be moved and scaled
    /// </summary>
    public class ScalableChartDrawer
    {
        public double CenterX { get; set; }
        public double CenterY { get; set; }
        public double ViewHeight { get; set; }
        public double ViewWidth { get; set; }

        Canvas canvas;

        public ScalableScaleCalculator Calculator { get; private set; }

        public ScalableChartDrawer(Canvas canvas)
        {
            this.canvas = canvas;
            Calculator = new ScalableScaleCalculator();
        }

        public void CalculateInitialScale(Canvas canvas, double min, double max, int dataLength)
        {
            Calculator.CalculateInitialScale(canvas.ActualWidth, canvas.ActualHeight, min, max, dataLength);
        }
    }
}
