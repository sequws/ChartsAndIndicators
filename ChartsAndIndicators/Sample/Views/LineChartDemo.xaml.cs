﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Sample.Views
{
    /// <summary>
    /// Interaction logic for LineChartDemo
    /// </summary>
    public partial class LineChartDemo : UserControl
    {
        Dictionary<string, List<double>> linesData = new Dictionary<string, List<double>>();

        public LineChartDemo()
        {
            InitializeComponent();
            GenerateNewLines();
        }

        private void RandomLinesButton_Click(object sender, RoutedEventArgs e)
        {
            GenerateNewLines();
        }

        private void GenerateNewLines()
        {
            Random rnd = new Random();
            Random rndDiff = new Random();
            try
            {
                linesData = new Dictionary<string, List<double>>();

                var linesNum = int.Parse(LinesTextBox.Text);
                var linesLength = int.Parse(MaxLengthTextBox.Text);
                var maxDiff = int.Parse(MaxDiffTextBox.Text);

                if (linesNum <= 0) linesNum = 1;

                for (int i = 0; i < linesNum; i++)
                {
                    double actVal = 0;
                    string name = $"Line {i}";
                    List<double> data = new List<double>();

                    for (int x = 0; x < linesLength; x++)
                    {
                        double diff = rnd.NextDouble() * maxDiff;
                        int dir = rndDiff.Next(-1, 1);
                        actVal += dir >= 0 ? diff : -diff;

                        data.Add(actVal);
                    }

                    linesData.Add(name, data);
                }

                MainLineChart.LinesData = linesData;
            }
            catch (Exception)
            {
                MessageBox.Show("Wrong parameters!");
            }
        }
    }
}
