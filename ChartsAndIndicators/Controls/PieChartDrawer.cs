using Controls.Models;
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
    public class PieChartDrawer
    {
        public double ChartMargin { get; set; } = 20;

        Canvas canvas;

        public PieChartDrawer(Canvas canvas)
        {
            this.canvas = canvas;
        }

        public void Draw(List<PiePart> data)
        {
            if (data.Count == 0) return;
            canvas.Children.Clear();

            DrawMultiPieChart(canvas, data);
        }

        public void DrawPercentChart(PiePart percentPart)
        {
            canvas.Children.Clear();
            var roundedVal = Math.Round(percentPart.Value);
            if (roundedVal == 0 || roundedVal == 100)
            {
                DrawFullCirce(canvas, percentPart);
            }
            else
            {
                DrawSinglePieChart(canvas, percentPart);
            }
        }

        private void DrawFullCirce(Canvas canvas, PiePart percentPart)
        {
            Image img = new Image();
            img.Width = 400;
            img.Height = 400;
            var r = img.Width / 2;

            var drawingImage = new DrawingImage();
            var drawingGroup = new DrawingGroup();

            drawingImage.Drawing = drawingGroup;

            var roundedVal = Math.Round(percentPart.Value);
            var brush = roundedVal == 0 ? new SolidColorBrush(Colors.Gray) : percentPart.Color;

            var drawing = new GeometryDrawing { Brush = brush };
            var pathGeometry = new PathGeometry();

            drawing.Geometry = pathGeometry;
            var eclipe = new EllipseGeometry(new Point(r, r), r, r);
            pathGeometry.AddGeometry(eclipe);

            drawingGroup.Children.Add(drawing);

            img.Source = drawingImage;
            canvas.Children.Add(img);
        }

        private List<double> CalculatePartPercentage(List<PiePart> data)
        {
            List<double> percentParts = new List<double>();
            var sum = data.Sum(x => x.Value);

            foreach (var part in data)
            {
                double percent = (part.Value / sum)*100;
                percentParts.Add(percent);
            }

            return percentParts;
        }

        private void DrawSinglePieChart(Canvas canvas, PiePart percentPart)
        {
            Image img = new Image();
            img.Width = 400;
            img.Height = 400;

            canvas.Background = new SolidColorBrush(Colors.Yellow);

            var width = img.Width; // controlWidth;
            var height = img.Height; // controlHeight;++
            var r = width / 2;

            var drawingImage = new DrawingImage();
            var drawingGroup = new DrawingGroup();

            drawingImage.Drawing = drawingGroup;

            var angle = 360 * (percentPart.Value / 100);
            var radians = (Math.PI / 180) * angle;
            var endPointX = Math.Sin(radians) * r + r;
            var endPointY = r - Math.Cos(radians) * r;
            var endPoint = new Point(endPointX, endPointY);

            var centerPoint = new Point(r, r);
            var startPoint = new Point(centerPoint.X, 0);

            var drawing = new GeometryDrawing { Brush = percentPart.Color };
            var pathGeometry = new PathGeometry();
            var pathFigure = new PathFigure { StartPoint = centerPoint };

            var ls1 = new LineSegment(startPoint, false);
            var arc = new ArcSegment
            {
                SweepDirection = SweepDirection.Clockwise,
                Size = new Size(r, r),
                Point = endPoint,
                IsLargeArc = angle > 180
            };
            var ls2 = new LineSegment(centerPoint, false);

            drawing.Geometry = pathGeometry;
            pathGeometry.Figures.Add(pathFigure);
            pathFigure.Segments.Add(ls1);
            pathFigure.Segments.Add(arc);
            pathFigure.Segments.Add(ls2);

            drawingGroup.Children.Add(drawing);

            var drawing2 = new GeometryDrawing { Brush = new SolidColorBrush(Colors.Gray) };
            var pathGeometry2 = new PathGeometry();
            var pathFigure2 = new PathFigure { StartPoint = centerPoint };

            var angle2 = 360; 
            var radians2 = (Math.PI / 180) * angle2;
            var endPointX2 = Math.Sin(radians2) * r + r;
            var endPointY2 = r - Math.Cos(radians2) * r;
            var endPoint2 = new Point(endPointX2, endPointY2);

            var ls12 = new LineSegment(endPoint, false);
            var arc22 = new ArcSegment
            {
                SweepDirection = SweepDirection.Clockwise,
                Size = new Size(r, r),
                Point = endPoint2,
                IsLargeArc = 360 - angle > 180
            };
            var ls22 = new LineSegment(centerPoint, false);

            drawing2.Geometry = pathGeometry2;
            pathGeometry2.Figures.Add(pathFigure2);
            pathFigure2.Segments.Add(ls12);
            pathFigure2.Segments.Add(arc22);
            pathFigure2.Segments.Add(ls22);

            drawingGroup.Children.Add(drawing2);
            DrawCircle(canvas, endPoint.X, endPoint.Y, 5, Brushes.Blue);

            img.Source = drawingImage;
            canvas.Children.Add(img);
        }

        private void DrawMultiPieChart(Canvas canvas, List<PiePart> data)
        {
            var percents = CalculatePartPercentage(data);

            Image img = new Image();
            img.Width = 400;
            img.Height = 400;

            canvas.Background = new SolidColorBrush(Colors.GreenYellow);

            var width = img.Width; // controlWidth;
            var height = img.Height; // controlHeight;++
            var r = width / 2;

            var drawingImage = new DrawingImage();
            var drawingGroup = new DrawingGroup();

            drawingImage.Drawing = drawingGroup;

            var centerPoint = new Point(r, r);
            var startPoint = new Point(centerPoint.X, 0);

            double angle = 0;
            double sumAngle = 0;

            int i = 0;
            foreach (var item in data)
            {
                var percent = percents[i];
                angle = 360 * (percent / 100); // must add previous value
                sumAngle += angle;

                if (i == data.Count - 1) sumAngle = 360;
                i++;

                var radians = (Math.PI / 180) * sumAngle;
                var endPointX = Math.Sin(radians) * r + r;
                var endPointY = r - Math.Cos(radians) * r;

                var endPoint = new Point(endPointX, endPointY);
                var drawing = new GeometryDrawing { Brush = item.Color };
                var pathGeometry = new PathGeometry();
                var pathFigure = new PathFigure { StartPoint = centerPoint };

                var ls1 = new LineSegment(startPoint, false);
                var arc = new ArcSegment
                {
                    SweepDirection = SweepDirection.Clockwise,
                    Size = new Size(r, r),
                    Point = endPoint,
                    IsLargeArc = angle > 180
                };
                var ls2 = new LineSegment(centerPoint, false);

                drawing.Geometry = pathGeometry;
                pathGeometry.Figures.Add(pathFigure);
                pathFigure.Segments.Add(ls1);
                pathFigure.Segments.Add(arc);
                pathFigure.Segments.Add(ls2);

                drawingGroup.Children.Add(drawing);

                startPoint = endPoint;
            }

            img.Source = drawingImage;
            canvas.Children.Add(img);
        }

        public static void DrawCircle(Canvas canvas, double x, double y, double r, SolidColorBrush color)
        {
            Ellipse circle = new Ellipse()
            {
                Width = r,
                Height = r,
                Stroke = color,
                StrokeThickness = 6
            };

            canvas.Children.Add(circle);

            circle.SetValue(Canvas.LeftProperty, (double)x);
            circle.SetValue(Canvas.TopProperty, (double)y);
        }
    }
}
