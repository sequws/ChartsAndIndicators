using System;
using System.Collections.Generic;
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
        }

        private void RandomLinesButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Random rnd = new Random();
            //MainLineChart.Reset();

            int lines = rnd.Next(5, 10);
            for (int i = 0; i < lines; i++)
            {
                //MainLineChart.AddBar(BarNameTextBox.Text + i, rnd.Next(-200, 200));
            }
        }
    }
}
