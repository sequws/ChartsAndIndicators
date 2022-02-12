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
        private string _chartName;
        public string ChartName
        {
            get => _chartName;
            set => SetField(ref _chartName, value);
        }

        private Dictionary<string,double> _barValues = new Dictionary<string, double>();

        public Dictionary<string,double> BarValues
        {
            get { return _barValues; }
            set
            {
                _barValues = value;
                SetField(ref _barValues, value);
            }
        }
        
        public BarChart()
        {
            InitializeComponent();

            DataContext = this;
            ChartName = "Bar Chart";

            BarValues.Add("foo", 20);
            BarValues.Add("bar", 40);
        }

        private void DrawMainRect(int margin = 10)
        {
            MainCanvas.Children.Clear();

            int i = 0;
            foreach(var kvpBar in BarValues)
            {
                var barX = (MainCanvas.ActualWidth - BarValues.Count * 40) / 2;
                var barY = MainCanvas.ActualHeight - 100;
                Rectangle rect = new Rectangle();
                rect.Height = kvpBar.Value * 10;
                rect.Width = 40;
                rect.Stroke = Brushes.Blue;
                rect.StrokeThickness = 2;
                Canvas.SetLeft(rect, barX + i * 50);
                Canvas.SetTop(rect, barY - rect.Height);

                var text = new TextBlock()
                {
                    Text = kvpBar.Key,
                };

                Canvas.SetLeft(text, barX + i *40);
                Canvas.SetTop(text, 90);

                MainCanvas.Children.Add(rect);
                MainCanvas.Children.Add(text);
                i ++;
            }
        }

        #region events
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DrawMainRect();
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
