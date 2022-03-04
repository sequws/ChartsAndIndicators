﻿using Controls.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Controls
{
    public class LineChartDrawer : ChartDrawer
    {
        Canvas canvas;

        public LineChartDrawer( Canvas canvas) : base( canvas)
        {
            this.canvas = canvas;
        }

        public void DrawLines(Dictionary<string, Dictionary<int, double>> linesData)
        {
            if (linesData.Count == 0) return;

            double max = 0;
            double min = 0;
            int dataLength = 0;

            foreach (var lineData in linesData)
            {
                var MaximumValue = lineData.Value.Max(x => x.Value);
                var MinimumValue = lineData.Value.Min(x => x.Value);

                max = MaximumValue > max ? MaximumValue : max;
                min = MinimumValue < min ? MinimumValue : min;
                if (lineData.Value.Count > dataLength) dataLength = lineData.Value.Count;
            }

            CalculateScale(min,max, dataLength);
            Draw();
        }

        public override void Draw()
        {
            base.Draw();

            Line line = new Line();
            line.X1 = 10;
            line.X2 = 100;
            line.Y1 = 10 * ScaleCalculator.Scale;
            line.Y2 = 50 * ScaleCalculator.Scale;
            line.Stroke = Brushes.OrangeRed;
            line.StrokeThickness = 2;


            canvas.Children.Add(line);

        }
    }
}