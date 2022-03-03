using Controls.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Controls
{
    /// <summary>
    /// Interaction logic for LineChart.xaml
    /// </summary>
    public partial class LineChart : UserControl
    {
        private string _chartName = "LineChart";
        public string ChartName
        {
            get => _chartName;
            set => SetField(ref _chartName, value);
        }

        ScaleCalculator scaleCalculator;
        LineChartDrawer chartDrawer;

        public LineChart()
        {
            InitializeComponent();

            DataContext = this;

            scaleCalculator = new ScaleCalculator(MainCanvas);
            chartDrawer = new LineChartDrawer(scaleCalculator, MainCanvas);


            var line1data = new Dictionary<int, double>();
            line1data.Add(1, -12.06);
            line1data.Add(2, -6.88);
            line1data.Add(3, 8.22);
            line1data.Add(4, 16.75);
            line1data.Add(5, 42.12);


            LinesData.Add("Line1", line1data);
        }

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //scaleCalculator.CalculateScale(-50, 100, 2);
            
            chartDrawer.DrawLines( LinesData);
            chartDrawer.Draw();
        }

        private Dictionary<string, Dictionary<int,double>> _linesData = new Dictionary<string, Dictionary<int, double>>();

        public Dictionary<string, Dictionary<int, double>> LinesData
        {
            get { return _linesData; }
            set
            {
                SetField(ref _linesData, value);
            }
        }


        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        #endregion
    }
}
