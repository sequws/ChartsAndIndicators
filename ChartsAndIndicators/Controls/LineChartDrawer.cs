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
    public class LineChartDrawer : ChartDrawer
    {
        Canvas canvas;

        public LineChartDrawer(Canvas canvas) : base(canvas)
        {
            this.canvas = canvas;
        }

        public void Draw(Dictionary<string, List<double>> linesData)
        {
            if (linesData.Count == 0) return;

            double max = 0;
            double min = 0;
            int dataLength = 0;

            foreach (var lineData in linesData)
            {
                var MaximumValue = lineData.Value.Max();
                var MinimumValue = lineData.Value.Min();

                max = MaximumValue > max ? MaximumValue : max;
                min = MinimumValue < min ? MinimumValue : min;
                if (lineData.Value.Count > dataLength) dataLength = lineData.Value.Count;
            }

            CalculateScale(min, max, dataLength);
            Draw();
            DrawLines(linesData);
        }

        public void DrawLines(Dictionary<string, List<double>> linesData)
        {
            Brush[] brushes = { Brushes.Red, Brushes.Green , Brushes.Blue, Brushes.Yellow, Brushes.Orange, Brushes.DarkGray, Brushes.Fuchsia };

            double stepX = ScaleCalculator.CanvasWidth / ScaleCalculator.DataLength;

            int brushNum = 0;
            foreach (var lineData in linesData)
            {
                PointCollection points = new PointCollection();
                int i = 0;
                foreach(var data in lineData.Value)
                {
                    points.Add(new Point(i * stepX, ScaleCalculator.LineZeroY - data * ScaleCalculator.Scale));
                    i++;
                }

                Polyline polyline = new Polyline();
                polyline.StrokeThickness = 2;
                polyline.Stroke = brushes[brushNum % brushes.Count()];
                polyline.Points = points;

                canvas.Children.Add(polyline);
                brushNum++;
            }

        }
    }
}
