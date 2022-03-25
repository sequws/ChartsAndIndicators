using Controls;
using Controls.Models;
using System.Collections.Generic;
using System.Windows.Controls;

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

            MainOhlcChart.OhlcData = ohlcCandles;
        }
    }
}
