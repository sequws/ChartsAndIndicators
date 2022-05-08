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

            //CalculatePartPercentage(data);
            //CreateArcSegment(canvas);
            PercentPie(canvas, 25);
        }

        private void CalculatePartPercentage(List<PiePart> data)
        {
            List<double> percentParts = new List<double>();
            var sum = data.Sum(x => x.Value);

            foreach( var part in data)
            {
                double percent = part.Value / sum;
                percentParts.Add(percent);
            }
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

        private void PercentPie(Canvas canvas, double percent)
        {
            var controlWidth = canvas.ActualWidth;
            var controlHeight = canvas.ActualHeight;

            canvas.Background = new SolidColorBrush(Colors.Yellow);

            var width = 400; // controlWidth;
            var height = 400; // controlHeight;

            var drawingImage = new DrawingImage();
            var drawingGroup = new DrawingGroup();

            drawingImage.Drawing = drawingGroup;

            var centerPoint = new Point(controlWidth/ 2 , (controlHeight) / 2);
            var startPoint = new Point(centerPoint.X, centerPoint.Y - 200);

            DrawCircle(canvas, centerPoint.X, centerPoint.Y, 5, Brushes.Red);
            DrawCircle(canvas, startPoint.X, startPoint.Y, 5, Brushes.Green);

            var angle = 360 * (percent / 100);
            var radians = (Math.PI / 180) * angle;
            var endPointX = Math.Sin(radians) * height / 2 + centerPoint.X;
            var endPointY = centerPoint.Y - Math.Cos(radians) * width / 2;
            var endPoint = new Point(endPointX, endPointY);

            DrawCircle(canvas, endPoint.X, endPoint.Y, 5, Brushes.Blue);




            var drawing = new GeometryDrawing { Brush = new SolidColorBrush(Colors.Red) };
            var pathGeometry = new PathGeometry();
            var pathFigure = new PathFigure { StartPoint = centerPoint };

            var ls1 = new LineSegment(startPoint, false);
            var arc = new ArcSegment
            {
                SweepDirection = SweepDirection.Clockwise,
                Size = new Size(width / 2, height / 2),
                Point = endPoint,
                IsLargeArc = percent > 50
            };
            var ls2 = new LineSegment(centerPoint, false);

            drawing.Geometry = pathGeometry;
            pathGeometry.Figures.Add(pathFigure);
            pathFigure.Segments.Add(ls1);
            pathFigure.Segments.Add(arc);
            pathFigure.Segments.Add(ls2);

            drawingGroup.Children.Add(drawing);

            Image img = new Image();
            img.Source = drawingImage;

            canvas.Children.Add( img);
        }

        private void CreateArcSegment(Canvas canvas)
        {
            var controlWidth = canvas.ActualWidth;
            var controlHeight = canvas.ActualHeight;
            var centerPoint = new Point(controlWidth / 2, controlHeight / 2);

            PathFigure pthFigure = new PathFigure();
            pthFigure.StartPoint = new Point(centerPoint.X -50, centerPoint.Y); ;

            ArcSegment arcSeg = new ArcSegment();
            arcSeg.Point = new Point(centerPoint.X + 50 , centerPoint.Y);
            arcSeg.Size = new Size(50, 50);
            arcSeg.IsLargeArc = true;
            arcSeg.SweepDirection = SweepDirection.Counterclockwise;
            arcSeg.RotationAngle = 30;

            PathSegmentCollection myPathSegmentCollection = new PathSegmentCollection();
            myPathSegmentCollection.Add(arcSeg);

            pthFigure.Segments = myPathSegmentCollection;

            PathFigureCollection pthFigureCollection = new PathFigureCollection();
            pthFigureCollection.Add(pthFigure);

            PathGeometry pthGeometry = new PathGeometry();
            pthGeometry.Figures = pthFigureCollection;

            Path arcPath = new Path();
            arcPath.Stroke = new SolidColorBrush(Colors.Black);
            arcPath.StrokeThickness = 1;
            arcPath.Data = pthGeometry;
            arcPath.Fill = new SolidColorBrush(Colors.Yellow);

            canvas.Children.Add(arcPath);
        }
    }
}
