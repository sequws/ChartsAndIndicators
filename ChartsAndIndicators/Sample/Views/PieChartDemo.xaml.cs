using Controls.Models;
using System.Collections.Generic;
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
                  
            MainChart.PieData = FillExampleData();
        }

        List<PiePart> FillExampleData()
        {
            List<PiePart> pieParts = new List<PiePart>();

            pieParts.Add(new PiePart(1, "Hungary", 9.75, new SolidColorBrush(Colors.Blue)));
            pieParts.Add(new PiePart(1, "Poland", 37.8, new SolidColorBrush(Colors.Red)));
            pieParts.Add(new PiePart(1, "Germany", 83.78, new SolidColorBrush(Colors.Brown)));

            return pieParts;
        }

        private void PercentSlider_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            var val = PercentSlider.Value;
            PiePart piePart = new PiePart(0, "test", val, new SolidColorBrush(Colors.Blue));
            if(MainChart != null)
            {
                MainChart.PieData = new List<PiePart> { piePart };
            }
        }            
    }
}
