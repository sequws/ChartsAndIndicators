using Controls;
using Controls.Models;
using System;
using System.Collections.Generic;
using System.Windows;
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
        Random rnd = new Random();
        Random rndDiff = new Random();

        public OhlcChartDemo()
        {
            InitializeComponent();

            DataContext = this;

            GenerateData();

            MainOhlcChart.OhlcData = ohlcCandles;
            MainOhlcChart.ChartBackground = new SolidColorBrush(Colors.WhiteSmoke);

            MainOhlcChart.DecimalPlaces = 2;
        }

        private void GenerateData()
        {
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

            ohlcCandles.Add(new Ohlc(116, 134, 116, 128));
            ohlcCandles.Add(new Ohlc(128, 138, 126, 136));
            ohlcCandles.Add(new Ohlc(136, 138, 130, 134));
            ohlcCandles.Add(new Ohlc(134, 140, 134, 140));
            ohlcCandles.Add(new Ohlc(140, 140, 124, 126));
        }

        private void RandomCandlesButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            GenerateRandomCandles();
        }

        void GenerateRandomCandles()
        {
            try
            {
                ohlcCandles = new List<Ohlc>();
                var numCandles = int.Parse(LinesTextBox.Text);
                double start = rnd.Next(0, 200);
                var maxDiff = double.Parse( MaxDiffTextBox.Text);

                for (int i = 0; i < numCandles;i++)
                {
                    var ohlc = GenerateNewCandle(rnd, rndDiff, start, maxDiff);
                    ohlcCandles.Add(ohlc);
                    start = ohlc.Close;
                }

                MainOhlcChart.OhlcData = ohlcCandles;
                MainOhlcChart.ChartBackground = new SolidColorBrush(Colors.WhiteSmoke);
            }
            catch (Exception e)
            {
                MessageBox.Show("Wrong parameters! " + e);
            }
        }

        private void AddCandleButton_Click(object sender, RoutedEventArgs e)
        {
            if (ohlcCandles.Count == 0) return;
            var start = ohlcCandles[ohlcCandles.Count - 1].Close;

            var maxDiff = double.Parse(MaxDiffTextBox.Text);
            var ohlc = GenerateNewCandle(rnd, rndDiff, start, maxDiff);
            ohlcCandles.Add(ohlc);
            start = ohlc.Close;

            MainOhlcChart.OhlcData = ohlcCandles;
        }

        private Ohlc GenerateNewCandle(Random rnd, Random rndDiff, double open, double maxDiff)
        {
            Ohlc candle = new Ohlc();

            var diff = rnd.NextDouble() * maxDiff;
            var dir = rndDiff.NextDouble();
            var nextOpen = dir > 0.5 ? open + diff : open - diff;

            candle.Open = open;
            candle.High = nextOpen + diff;
            candle.Low = open - diff;
            candle.Close = nextOpen;

            return candle;
        }
    }
}
