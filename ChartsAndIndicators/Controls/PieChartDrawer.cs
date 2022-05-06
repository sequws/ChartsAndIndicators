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

            var sum = data.Sum(x => x.Value);
            CreateArcSegment(canvas);

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
            arcSeg.RotationAngle = 0;

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
