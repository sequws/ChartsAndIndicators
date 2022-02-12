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
    /// Interaction logic for BarChart.xaml
    /// </summary>
    public partial class BarChart : UserControl, INotifyPropertyChanged
    {
        private string _chartName;
        public string ChartName
        {
            get => _chartName;
            set => SetField(ref _chartName, value);
        }

        public BarChart()
        {
            InitializeComponent();

            DataContext = this;
            ChartName = "Bar Chart";  
        }

        private void DrawMainRect(int margin = 10)
        {
            //MainCanvas.Children.Clear();
            //Rectangle rect = new Rectangle();
            //Canvas.SetLeft(rect, margin);
            //Canvas.SetTop(rect, margin);
            //rect.Height = MainCanvas.ActualHeight - 2 * margin;
            //rect.Width = MainCanvas.ActualWidth - 2 * margin;
            //rect.Stroke = Brushes.Gray;
            //rect.StrokeThickness = 2;
            //MainCanvas.Children.Add(rect);
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

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DrawMainRect();
        }
    }
}
