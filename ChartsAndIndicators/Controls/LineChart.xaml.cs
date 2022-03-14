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
