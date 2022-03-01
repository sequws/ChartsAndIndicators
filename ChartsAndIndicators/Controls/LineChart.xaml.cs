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

        public LineChart()
        {
            InitializeComponent();

            DataContext = this;

            scaleCalculator = new ScaleCalculator(MainCanvas);
            scaleCalculator.CalculateScale(-50, 100, 2);
        }

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            scaleCalculator.CalculateScale(-50, 100, 2);
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
