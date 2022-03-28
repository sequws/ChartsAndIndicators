using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Controls.Common
{

    public class ScalableScaleCalculator
    {
        public double Scale { get; set; }
        public Point Center { get; set; }

        // without canvas reference
        private double ctrlWidth = 0;
        private double ctrlHeight = 0;

        // visible area on chart
        private double viewHeight = 0;
        private double viewWidth = 0;

        // highest and lowest point from data
        private double dataHigh;
        private double dataLow;
        private double dataLength;

    }
}
