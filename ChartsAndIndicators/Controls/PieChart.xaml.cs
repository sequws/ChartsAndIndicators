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
    /// Interaction logic for PieChart.xaml
    /// </summary>
    public partial class PieChart : UserControl, INotifyPropertyChanged
    {
        #region properties
        private string _chartName = "PieChart";
        public string ChartName
        {
            get => _chartName;
            set => SetField(ref _chartName, value);
        }

        private SolidColorBrush _chartBackground = new SolidColorBrush(Colors.AliceBlue);
        public SolidColorBrush ChartBackground
        {
            get => _chartBackground;
            set => SetField(ref _chartBackground, value);
        }

        private List<PiePart> _pieData = new List<PiePart>();

        public List<PiePart> PieData
        {
            get { return _pieData; }
            set
            {
                SetField(ref _pieData, value);
                // todo refresh chart
            }
        }
        #endregion

        PieChartDrawer pieChartDrawer;

        public PieChart()
        {
            InitializeComponent();

            DataContext = this;
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
