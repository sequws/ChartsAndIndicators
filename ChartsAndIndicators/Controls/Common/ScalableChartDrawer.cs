using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

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

            DrawAxisY();
        }

        private void DrawAxisY()
        {
            Line axisY = new Line();

            axisY.X1 = 10;
            axisY.X2 = 10;
            axisY.Y1 = (Calculator.DataLow - Calculator.ViewMin) * Calculator.InitialScale;
            axisY.Y2 = (Calculator.DataHigh - Calculator.ViewMin) * Calculator.InitialScale;

            axisY.Stroke = Brushes.Fuchsia;
            axisY.StrokeThickness = 2;
            canvas.Children.Add(axisY);
        }
    }
}
