using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Controls
{
    /// <summary>
    /// Interaction logic for BarChart.xaml
    /// </summary>
    public partial class BarChart : UserControl, INotifyPropertyChanged
    {
        #region properties
        private string _chartName;
        public string ChartName
        {
            get => _chartName;
            set => SetField(ref _chartName, value);
        }

        private bool _isValueVIsible;

        public bool IsValueVisible
        {
            get => _isValueVIsible;
            set => SetField(ref _isValueVIsible, value);
        }

        private bool _isDescVisible;

        public bool IsDescVisible
        {
            get => _isDescVisible;
            set => SetField(ref _isDescVisible, value);
        }

        private SolidColorBrush _chartBackground = new SolidColorBrush( Colors.AliceBlue);

        public SolidColorBrush ChartBackground
        {
            get => _chartBackground;
            set => SetField(ref _chartBackground, value);
        }

        private Dictionary<string, double> _barValues = new Dictionary<string, double>();

        public Dictionary<string, double> BarValues
        {
            get { return _barValues; }
            set
            {
                SetField(ref _barValues, value);
            }
        }
        #endregion

        BarChartDrawer barChartDrawer;

        public BarChart()
        {
            InitializeComponent();

            DataContext = this;
            ChartName = "Bar Chart";

            barChartDrawer = new BarChartDrawer(MainCanvas);
        }

        public void Reset()
        {
            BarValues.Clear();
        }

        public void AddBar(string name, double value)
        {
            if (!BarValues.ContainsKey(name))
            {
                BarValues.Add(name, value);
                barChartDrawer.Draw(BarValues);
            }
        }


        #region events

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            barChartDrawer.Draw(BarValues);
        }

        #endregion

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
