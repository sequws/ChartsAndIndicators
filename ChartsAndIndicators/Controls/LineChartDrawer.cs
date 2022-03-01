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
        ScaleCalculator scaleCalculator;

        public LineChartDrawer(ScaleCalculator scaleCalculator, Canvas canvas) : base(scaleCalculator, canvas)
        {
            this.canvas = canvas;
            this.scaleCalculator = scaleCalculator;
        }

        public override void Draw()
        {
            base.Draw();

            Line line = new Line();
            line.X1 = 10;
            line.X2 = 100;
            line.Y1 = 10 * scaleCalculator.Scale;
            line.Y2 = 50 * scaleCalculator.Scale;
            line.Stroke = Brushes.OrangeRed;
            line.StrokeThickness = 2;


            canvas.Children.Add(line);

        }
    }
}
