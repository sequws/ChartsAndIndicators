using Controls.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Sample.Views
{
    /// <summary>
    /// Interaction logic for PieChartDemo.xaml
    /// </summary>
    public partial class PieChartDemo : UserControl
    {
        public PieChartDemo()
        {
            InitializeComponent();
                  
            MainPercentChart.PercentData = new PiePart(0, "test", 25, new SolidColorBrush(Colors.Blue)); ;
            MainMultiChart.PieData = FillExampleData();
        }

        List<PiePart> FillExampleData()
        {
            List<PiePart> pieParts = new List<PiePart>();

            //pieParts.Add(new PiePart(1, "Hungary", 50, new SolidColorBrush(Colors.Blue)));
            //pieParts.Add(new PiePart(2, "Poland", 50, new SolidColorBrush(Colors.Red)));
            //pieParts.Add(new PiePart(3, "Spain", 100, new SolidColorBrush(Colors.Orange)));

            // Test 
            //pieParts.Add(new PiePart(1, "Hungary", 33, new SolidColorBrush(Colors.Blue)));
            //pieParts.Add(new PiePart(2, "Poland", 33, new SolidColorBrush(Colors.Red)));
            //pieParts.Add(new PiePart(3, "Spain", 33, new SolidColorBrush(Colors.Orange)));

            pieParts.Add(new PiePart(1, "Hungary", 9.75, new SolidColorBrush(Colors.Blue)));
            pieParts.Add(new PiePart(2, "Poland", 37.8, new SolidColorBrush(Colors.Red)));
            pieParts.Add(new PiePart(3, "Germany", 83.78, new SolidColorBrush(Colors.LightSalmon)));
            pieParts.Add(new PiePart(4, "India", 112.78, new SolidColorBrush(Colors.Orange)));

            return pieParts;
        }

        private void PercentSlider_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            var val = PercentSlider.Value;
            PiePart piePart = new PiePart(0, "test", val, new SolidColorBrush(Colors.Blue));
            if(MainPercentChart != null)
            {
                MainPercentChart.PercentData = piePart; //new List<PiePart> { piePart };
                PercentValueLabel.Content = $"{val.ToString("F2")}%";
            }
        }

        private void RandomPercentButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            var val = rnd.NextDouble() * 100;
            PiePart piePart = new PiePart(0, "test", val, new SolidColorBrush(Colors.Blue));
            if (MainPercentChart != null)
            {
                MainPercentChart.PercentData = piePart; 
                PercentValueLabel.Content = $"{val.ToString("F2")}%";
            }
        }

        private void RandomMultiPartsButton_Click(object sender,RoutedEventArgs e)
        {
            Brush[] brushes = { Brushes.Red, Brushes.Green, Brushes.Blue, Brushes.Yellow, Brushes.Orange, Brushes.DarkGray, Brushes.Fuchsia };

            try
            {
                List<PiePart> pieParts = new List<PiePart>();
                Random rnd = new Random(Guid.NewGuid().GetHashCode());

                var parts = int.Parse(PartsTextBox.Text);

                for (int i = 0; i < parts; i++)
                {
                    var val = rnd.Next(0, 100);
                    pieParts.Add(new PiePart(i, $"Part {i}", val, brushes[i % brushes.Length]));
                }

                MainMultiChart.PieData = pieParts;
            }
            catch (Exception)
            {
                MessageBox.Show("Wrong parameters!");
            }
        }
    }
}
