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
            //PercentPie(canvas, data[0].Value);
            //PercentPieFullCircle(canvas, data[0].Value);
            PercentPieTest(canvas, data[0].Value);

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


        private void PercentPieTest(Canvas canvas, double percent)
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

            var angle = 360 * (percent / 100);
            var radians = (Math.PI / 180) * angle;
            var endPointX = Math.Sin(radians) * r + r;
            var endPointY = r - Math.Cos(radians) * r;
            var endPoint = new Point(endPointX, endPointY);

            var centerPoint = new Point(r,r);
            var startPoint = new Point(centerPoint.X, 0);

            var drawing = new GeometryDrawing { Brush = new SolidColorBrush(Colors.Red) };
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

            var angle2 = 360;// - angle; //360 * (360-percent / 100);
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
            //drawingGroup.Children.Add(CreatePathGeometry(r, new SolidColorBrush(Colors.Green), new Point(r, r), endPoint, percent > 0.5));
            //drawingGroup.Children.Add(CreatePathGeometry(new SolidColorBrush(Colors.Gray), endPoint, new Point(controlWidth / 2, 0), percent <= 0.5));

            img.Source = drawingImage;
            canvas.Children.Add(img);
        }



        private void PercentPieFullCircle(Canvas canvas, double percent)
        {
            //var controlWidth = canvas.ActualWidth;
            //var controlHeight = canvas.ActualHeight;

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

            var angle = 360 * (percent / 100);
            var radians = (Math.PI / 180) * angle;
            var endPointX = Math.Sin(radians) * r +r;
            var endPointY = r - Math.Cos(radians) * r;
            var endPoint = new Point(endPointX, endPointY);

            drawingGroup.Children.Add(CreatePathGeometry( r, new SolidColorBrush(Colors.Green), new Point(r,r), endPoint, percent > 0.5));
            //drawingGroup.Children.Add(CreatePathGeometry(new SolidColorBrush(Colors.Gray), endPoint, new Point(controlWidth / 2, 0), percent <= 0.5));

            img.Source = drawingImage;
            canvas.Children.Add(img);
        }


        private GeometryDrawing CreatePathGeometry(double r, Brush brush, Point startPoint, Point arcPoint, bool isLargeArc)
        {
            var midPoint = new Point(r, r);
            var drawing = new GeometryDrawing { Brush = brush };
            var pathGeometry = new PathGeometry();
            var pathFigure = new PathFigure { StartPoint = midPoint };

            var ls1 = new LineSegment(startPoint, false);
            var arc = new ArcSegment
            {
                SweepDirection = SweepDirection.Clockwise,
                Size = new Size(r, r),
                Point = arcPoint,
                IsLargeArc = isLargeArc
            };
            var ls2 = new LineSegment(midPoint, false);

            drawing.Geometry = pathGeometry;
            pathGeometry.Figures.Add(pathFigure);

            pathFigure.Segments.Add(ls1);
            pathFigure.Segments.Add(arc);
            pathFigure.Segments.Add(ls2);

            return drawing;
        }



        private void PercentPie(Canvas canvas, double percent)
        {
            var controlWidth = canvas.ActualWidth;
            var controlHeight = canvas.ActualHeight;

            canvas.Background = new SolidColorBrush(Colors.Yellow);

            var width = 200; // controlWidth;
            var height = 200; // controlHeight;++
            var r = 100;

            var drawingImage = new DrawingImage();
            var drawingGroup = new DrawingGroup();

            drawingImage.Drawing = drawingGroup;
            
            var centerPoint = new Point(controlWidth/ 2 , (controlHeight) / 2);
            var startPoint = new Point(centerPoint.X, centerPoint.Y - r);

            DrawCircle(canvas, centerPoint.X, centerPoint.Y, 5, Brushes.Red);
            DrawCircle(canvas, startPoint.X, startPoint.Y, 5, Brushes.Green);

            var angle = 360 * (percent / 100);
            var radians = (Math.PI / 180) * angle;
            var endPointX = Math.Sin(radians) * r + centerPoint.X;
            var endPointY = centerPoint.Y - Math.Cos(radians) * r;
            var endPoint = new Point(endPointX, endPointY);

            DrawCircle(canvas, endPoint.X, endPoint.Y, 5, Brushes.Blue);

            var drawing = new GeometryDrawing { Brush = new SolidColorBrush(Colors.Red) };  
            var pathGeometry = new PathGeometry();
            var pathFigure = new PathFigure { StartPoint = centerPoint };

            var ls1 = new LineSegment(startPoint, false);
            var arc = new ArcSegment
            {
                SweepDirection = SweepDirection.Clockwise,
                Size = new Size(r, r),
                Point = endPoint,
                IsLargeArc = percent > 50
            };
            var ls2 = new LineSegment(centerPoint, false);

            drawing.Geometry = pathGeometry;
            pathGeometry.Figures.Add(pathFigure);
            pathFigure.Segments.Add(ls1);
            pathFigure.Segments.Add(arc);
            pathFigure.Segments.Add(ls2);

            GeometryDrawing drawingRect = new GeometryDrawing(
                Brushes.Lime,
                new Pen(Brushes.Black, 2),
                new RectangleGeometry(new Rect(0,0,width,height))// new Size(width,height)))
                );

            drawingGroup.Children.Add(drawing);
            drawingGroup.Children.Add(drawingRect);
                        
            Image img = new Image();
            //img.Width = width;
            //img.Height = height;
            //img.Stretch = Stretch.None;
            img.Source = drawingImage;
            
            img.SetValue(Canvas.LeftProperty, (double)centerPoint.X - width /2);
            img.SetValue(Canvas.TopProperty, (double)centerPoint.Y - width /2);

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
