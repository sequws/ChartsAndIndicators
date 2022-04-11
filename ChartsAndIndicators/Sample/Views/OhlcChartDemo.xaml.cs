using Controls;
using Controls.Models;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;

namespace Sample.Views
{
    /// <summary>
    /// Interaction logic for OhlcChart
    /// </summary>
    public partial class OhlcChartDemo : UserControl
    {
        List<Ohlc> ohlcCandles = new List<Ohlc>();

        public OhlcChartDemo()
        {
            InitializeComponent();

            DataContext = this;

            ohlcCandles.Add(new Ohlc
            {
                Open = 121,
                High = 128,
                Low = 112,
                Close = 115
            });

            ohlcCandles.Add(new Ohlc(115, 117, 112, 116));
            ohlcCandles.Add(new Ohlc(116, 118, 114, 118));
            ohlcCandles.Add(new Ohlc(118, 122, 117, 120));
            ohlcCandles.Add(new Ohlc(120, 126, 120, 124));
            ohlcCandles.Add(new Ohlc(124, 126, 120, 124));
            ohlcCandles.Add(new Ohlc(124, 126, 115, 116));

            MainOhlcChart.OhlcData = ohlcCandles;
            MainOhlcChart.ChartBackground = new SolidColorBrush(Colors.WhiteSmoke);
        }
    }
}
