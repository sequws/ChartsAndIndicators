﻿using System;
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
        ScaleCalculator _scaleCalculator;
        Canvas canvas;

        bool scaleCalculated = false;

        // chart
        double canvasWidth;
        double TextHeight = 10;

        public bool DrawYAxisDesc { get; set; } = true;

        public ScaleCalculator ScaleCalculator
        {
            get
            {
                return _scaleCalculator;
            }
        }

        public ChartDrawer(Canvas canvas)
        {
            _scaleCalculator = new ScaleCalculator(canvas);
            this.canvas = canvas;
        }

        public void CalculateScale(double min, double max, int dataLength, int margin = 10)
        {
            _scaleCalculator.CalculateScale(min, max, dataLength, margin);
            scaleCalculated = true;
        }

        public virtual void Draw()
        {
            if (!scaleCalculated)
            {
                throw new Exception("Have to invoke ScaleCalculator.CalculateScale before Draw!");
            }

            canvasWidth = canvas.ActualWidth;

            DrawLineZero();
            DrawAxisY();

            if (DrawYAxisDesc)
            {
                DrawYAxisStepDesc();
            }
            else
            {
                DrawAxisYText();
            }
        }

        private void DrawLineZero()
        {
            canvas.Children.Clear();

            Line lineZero = new Line();
            lineZero.X1 = _scaleCalculator.CanvasMargin;
            lineZero.X2 = canvasWidth - _scaleCalculator.CanvasMargin;
            lineZero.Y1 = _scaleCalculator.LineZeroY;
            lineZero.Y2 = _scaleCalculator.LineZeroY;
            lineZero.Stroke = Brushes.Fuchsia;
            lineZero.StrokeThickness = 2;

            canvas.Children.Add(lineZero);
        }

        private void DrawAxisY()
        {
            Line axisY = new Line();
            axisY.X1 = _scaleCalculator.CanvasMargin;
            axisY.X2 = _scaleCalculator.CanvasMargin;
            axisY.Y1 = _scaleCalculator.LineZeroY - _scaleCalculator.MinH * _scaleCalculator.Scale;
            axisY.Y2 = _scaleCalculator.LineZeroY - _scaleCalculator.MaxH * _scaleCalculator.Scale;
            axisY.Stroke = Brushes.Black;
            axisY.StrokeThickness = 1;
            canvas.Children.Add(axisY);

            for (int i = 0; i < _scaleCalculator.AxisYStepsNum; i++)
            {
                Line step = new Line();
                step.Stroke = Brushes.LightGray;
                step.StrokeThickness = 1;
                var dashArray = new DoubleCollection();
                dashArray.Add(2);
                dashArray.Add(2);
                step.StrokeDashArray = dashArray;
                step.X1 = _scaleCalculator.CanvasMargin;
                step.X2 = canvasWidth - _scaleCalculator.CanvasMargin;
                step.Y1 = _scaleCalculator.StepBottomY - i * _scaleCalculator.StepHeight;
                step.Y2 = step.Y1;

                canvas.Children.Add(step);
            }
        }

        private void DrawYAxisStepDesc(double marginLeft = 10, int marginTop = 12)
        {
            for (int i = 0; i < _scaleCalculator.AxisYStepsNum; i++)
            {
                // lines desc between min max
                
                var text = new TextBlock();
                text.TextWrapping = TextWrapping.Wrap;
                text.Text = $"{ _scaleCalculator.MinH + i * _scaleCalculator.StepSizeVal}";

                text.FontSize = TextHeight;
                Canvas.SetLeft(text, _scaleCalculator.CanvasMargin + marginLeft);
                Canvas.SetTop(text, _scaleCalculator.StepBottomY - i * _scaleCalculator.StepHeight - marginTop);
                canvas.Children.Add(text);
            }
        }

        private void DrawAxisYText(double marginLeft = 10, int marginTop = 14)
        {
            var text = new TextBlock();
            text.TextWrapping = TextWrapping.Wrap;
            text.Text = $"{ _scaleCalculator.MaxH.ToString("N1")}";

            text.FontSize = TextHeight;
            Canvas.SetLeft(text, _scaleCalculator.CanvasMargin + marginLeft);
            Canvas.SetTop(text, _scaleCalculator.LineZeroY - _scaleCalculator.MaxH * _scaleCalculator.Scale - marginTop);

            var text2 = new TextBlock();
            text2.TextWrapping = TextWrapping.Wrap;
            text2.Text = $"{ _scaleCalculator.MinH.ToString("N1")}";

            text2.FontSize = TextHeight;
            Canvas.SetLeft(text2, _scaleCalculator.CanvasMargin + marginLeft);
            Canvas.SetTop(text2, _scaleCalculator.LineZeroY - _scaleCalculator.MinH * _scaleCalculator.Scale - marginTop);

            var text0 = new TextBlock();
            text0.Text = $"0";
            text0.FontSize = TextHeight;
            Canvas.SetLeft(text0, _scaleCalculator.CanvasMargin + marginLeft);
            Canvas.SetTop(text0, _scaleCalculator.LineZeroY - marginTop);

            canvas.Children.Add(text0);
            canvas.Children.Add(text);
            canvas.Children.Add(text2);
        }
    }
}
