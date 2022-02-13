using System.Windows.Controls;

namespace Sample.Views
{
    /// <summary>
    /// Interaction logic for DemoView1
    /// </summary>
    public partial class DemoView1 : UserControl
    {
        int barId = 0;

        public DemoView1()
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
                MainBarChart.AddBar(BarNameTextBox.Text + barId, double.Parse(BarValueTextBox.Text)+(barId*10));
                barId++;
            }
            catch (System.Exception)
            {
                throw;
            }
            
        }
    }
}
