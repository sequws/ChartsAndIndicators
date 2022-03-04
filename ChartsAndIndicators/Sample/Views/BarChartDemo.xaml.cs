using System;
using System.Windows.Controls;

namespace Sample.Views
{
    /// <summary>
    /// Interaction logic for DemoView1
    /// </summary>
    public partial class BarChartDemo : UserControl
    {
        int barId = 0;

        public BarChartDemo()
        {
            InitializeComponent();

            //MainBarChart.BarValues.Add("foo", 20);
            //MainBarChart.BarValues.Add("bar", 40);
        }

        private void AddBarButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            
            try
            {
                //MainBarChart.BarValues.Add( BarNameTextBox.Text,  int.Parse(BarValueTextBox.Text));
                MainBarChart.AddBar(BarNameTextBox.Text + barId, double.Parse(BarValueTextBox.Text));
                barId++;
                var val = double.Parse(BarValueTextBox.Text) + (barId * 5);
                BarValueTextBox.Text = val.ToString();
            }
            catch (System.Exception)
            {
                throw;
            }
            
        }

        private void RandomBarButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Random rnd = new Random();
            MainBarChart.Reset();

            int bars = rnd.Next(3, 10);
            for(int i = 0; i < bars; i++)
            {
                MainBarChart.AddBar(BarNameTextBox.Text + i, rnd.Next(-200,200));
            }
        }
    }
}
