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

        #region properties
        private string _chartName = "LineChart";
        public string ChartName
        {
            get => _chartName;
            set => SetField(ref _chartName, value);
        }

        private string _axisXDesc;
        public string AxisXDesc
        {
            get { return _axisXDesc; }
            set { _axisXDesc = value; }
        }

        private string _axisYDesc;
        public string AxisYDesc
        {
            get { return _axisYDesc; }
            set { _axisYDesc = value; }
        }
        #endregion

        LineChartDrawer chartDrawer;

        public LineChart()
        {
            InitializeComponent();

            DataContext = this;
            chartDrawer = new LineChartDrawer( MainCanvas);

            //var line1data = new List<double>();
            //line1data.Add(-12.06);
            //line1data.Add( -6.88);
            //line1data.Add(8.22);
            //line1data.Add(16.75);
            //line1data.Add(42.12);

            //var line2data = new List<double>();
            //line2data.Add(-10);
            //line2data.Add(5);
            //line2data.Add(13);
            //line2data.Add(11);
            //line2data.Add(2);
            //line2data.Add(12);
            //line2data.Add(-8);

            //var line3data = new List<double>();
            //line3data.Add(-5);
            //line3data.Add(5);
            //line3data.Add(-5);
            //line3data.Add(-5);
            //line3data.Add(5);
            //line3data.Add(5);
            //line3data.Add(5);
            //line3data.Add(-5);


            //LinesData.Add("Line1", line1data);
            //LinesData.Add("Line2", line2data);
            //LinesData.Add("Line3", line3data);
        }

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            chartDrawer.Draw( LinesData);
        }

        private Dictionary<string, List<double>> _linesData = new Dictionary<string, List<double>>();

        public Dictionary<string, List<double>> LinesData
        {
            get { return _linesData; }
            set
            {
                SetField(ref _linesData, value);
                chartDrawer.Draw(LinesData);
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
