using Controls.Models;
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
    /// Interaction logic for OhlcChart.xaml
    /// </summary>
    public partial class OhlcChart : UserControl, INotifyPropertyChanged
    {
        #region properties
        private string _chartName = "OhlcChart";
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

        private int _decimalPlaces;

        public int DecimalPlaces
        {
            get => _decimalPlaces;
            set
            {
                SetField(ref _decimalPlaces, value);
                ohlcChartDrawer.DecimalPlaces = value;
            }
        }

        private SolidColorBrush _chartBackground = new SolidColorBrush(Colors.AliceBlue);
        public SolidColorBrush ChartBackground
        {
            get => _chartBackground;
            set => SetField(ref _chartBackground, value);
        }
        #endregion

        private List<Ohlc> _ohlcData = new List<Ohlc>();

        public List<Ohlc> OhlcData
        {
            get { return _ohlcData; }
            set { 
                SetField(ref _ohlcData , value);
                ohlcChartDrawer.Draw(OhlcData);
            }
        }

        OhlcChartDrawer ohlcChartDrawer;

        public OhlcChart()
        {
            InitializeComponent();

            DataContext = this;
            ohlcChartDrawer = new OhlcChartDrawer(MainCanvas);
        }

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ohlcChartDrawer.Draw(OhlcData);
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
