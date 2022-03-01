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
    /// Draws the base of a graph based on scale
    /// </summary>
    public class ChartDrawer
    {
        ScaleCalculator _scaleCakculator;
        Canvas _canvas;

        // chart
        double canvasWidth;
        double TextHeight = 10;

        public ChartDrawer(ScaleCalculator scaleCalculator, Canvas canvas)
        {
            _scaleCakculator = scaleCalculator;
            _canvas = canvas;
        }

        public void Draw()
        {
            canvasWidth = _canvas.ActualWidth;

            DrawLineZero();
            DrawAxisY();
            DrawAxisYText();
        }

        private void DrawLineZero()
        {
            _canvas.Children.Clear();

            Line lineZero = new Line();
            lineZero.X1 = _scaleCakculator.CanvasMargin;
            lineZero.X2 = canvasWidth + _scaleCakculator.CanvasMargin;
            lineZero.Y1 = _scaleCakculator.LineZeroY;
            lineZero.Y2 = _scaleCakculator.LineZeroY;
            lineZero.Stroke = Brushes.Fuchsia;
            lineZero.StrokeThickness = 2;

            _canvas.Children.Add(lineZero);
        }

        private void DrawAxisY()
        {
            Line axisY = new Line();
            axisY.X1 = 0;
            axisY.X2 = 0;
            axisY.Y1 = _scaleCakculator.LineZeroY - _scaleCakculator.MinH * _scaleCakculator.Scale;
            axisY.Y2 = _scaleCakculator.LineZeroY - _scaleCakculator.MaxH * _scaleCakculator.Scale;
            axisY.Stroke = Brushes.Black;
            axisY.StrokeThickness = 1;
            _canvas.Children.Add(axisY);

            var steps = _scaleCakculator.AxisYStepsNum;
            var stepH = _scaleCakculator.ViewFullHeight * _scaleCakculator.Scale / (steps - 1);

            var stepBottomY = _scaleCakculator.LineZeroY - _scaleCakculator.LastStepY * stepH;

            for (int i = 0; i < steps; i++)
            {
                Line step = new Line();
                step.Stroke = Brushes.LightGray;
                step.StrokeThickness = 1;
                var dashArray = new DoubleCollection();
                dashArray.Add(2);
                dashArray.Add(2);
                step.StrokeDashArray = dashArray;
                step.X1 = 0;
                step.X2 = canvasWidth;
                step.Y1 = stepBottomY - i * stepH;
                step.Y2 = step.Y1;

                _canvas.Children.Add(step);
            }
        }

        private void DrawAxisYText(int marginLeft = 10, int marginTop = 14)
        {
            var text = new TextBlock();
            text.TextWrapping = TextWrapping.Wrap;
            text.Text = $"{ _scaleCakculator.MaxH.ToString("N1")}";

            text.FontSize = TextHeight;
            Canvas.SetLeft(text, marginLeft);
            Canvas.SetTop(text, _scaleCakculator.LineZeroY - _scaleCakculator.MaxH * _scaleCakculator.Scale - marginTop);

            var text2 = new TextBlock();
            text2.TextWrapping = TextWrapping.Wrap;
            text2.Text = $"{ _scaleCakculator.MinH.ToString("N1")}";

            text2.FontSize = TextHeight;
            Canvas.SetLeft(text2, marginLeft);
            Canvas.SetTop(text2, _scaleCakculator.LineZeroY - _scaleCakculator.MinH * _scaleCakculator.Scale - marginTop);

            var text0 = new TextBlock();
            text0.Text = $"0";
            text0.FontSize = TextHeight;
            Canvas.SetLeft(text0, marginLeft);
            Canvas.SetTop(text0, _scaleCakculator.LineZeroY - marginTop);

            _canvas.Children.Add(text0);
            _canvas.Children.Add(text);
            _canvas.Children.Add(text2);
        }
    }
}
