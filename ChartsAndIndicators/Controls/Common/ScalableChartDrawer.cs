using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        public int DecimalPlaces { get; set; } = 5;

        Canvas canvas;

        public ScalableScaleCalculator Calculator { get; private set; }

        private double descWidth = 70;
        private double axisYStepWidth = 5;

        public ScalableChartDrawer(Canvas canvas)
        {
            this.canvas = canvas;
            Calculator = new ScalableScaleCalculator();
        }

        public void CalculateInitialScale(Canvas canvas, double min, double max, int dataLength)
        {
            Calculator.CalculateInitialScale(canvas.ActualWidth, canvas.ActualHeight, min, max, dataLength);
            
            canvas.Children.Clear();
            DrawAxisY();
            DrawYAxisStepDesc();
        }

        private void DrawAxisY()
        {
            Line axisY = new Line();

            axisY.X1 = canvas.ActualWidth - descWidth;
            axisY.X2 = axisY.X1;
            axisY.Y1 = Calculator.CalcY(Calculator.DataLow);
            axisY.Y2 = Calculator.CalcY(Calculator.DataHigh);

            axisY.Stroke = Brushes.Fuchsia;
            axisY.StrokeThickness = 2;
            canvas.Children.Add(axisY);

            // axis Y steps
            for (int i = 0; i <= Calculator.MaxStepsOnYAxis; i++)
            {
                Line step = new Line();
                step.Stroke = Brushes.Black;
                step.StrokeThickness = 1;
                step.X1 = axisY.X1 - axisYStepWidth;
                step.X2 = axisY.X1 + axisYStepWidth;
                step.Y1 = Calculator.CalcY( Calculator.DataLow + i * Calculator.StepHeightOnYAxis);
                step.Y2 = step.Y1;

                // dash lines 
                Line stepLine = new Line();
                stepLine.Stroke = Brushes.LightGray;
                stepLine.StrokeThickness = 1;

                var dashArray = new DoubleCollection();
                dashArray.Add(2);
                dashArray.Add(2);
                stepLine.StrokeDashArray = dashArray;
                stepLine.X1 = axisYStepWidth;  // margin?
                stepLine.X2 = canvas.ActualWidth - descWidth - axisYStepWidth;
                stepLine.Y1 = Calculator.CalcY(Calculator.DataLow + i * Calculator.StepHeightOnYAxis);
                stepLine.Y2 = stepLine.Y1;

                canvas.Children.Add(step);
                canvas.Children.Add(stepLine);
            }
        }

        private void DrawYAxisStepDesc(double marginLeft = 10, int marginTop = 12)
        {
            for (int i = 0; i <= Calculator.MaxStepsOnYAxis; i++)
            {
                // lines desc between min max
                var text = new TextBlock();
                text.TextWrapping = TextWrapping.Wrap;
                var val = Calculator.DataLow + i * Calculator.StepHeightOnYAxis;
                text.Text = $"{ string.Format("{0:N" + DecimalPlaces + "}", val)}";

                text.FontSize = 8;
                Canvas.SetLeft(text, canvas.ActualWidth - descWidth + axisYStepWidth);
                Canvas.SetTop(text, Calculator.CalcY(Calculator.DataLow + i * Calculator.StepHeightOnYAxis) - text.FontSize);
                canvas.Children.Add(text);
            }
        }
    }
}
