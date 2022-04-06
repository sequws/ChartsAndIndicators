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
        private double initialScale = 0;    // Scale calculated on start to best fit data 

        // zooming eg mouse scroll
        private double zoom { get; set; }

        // without canvas reference
        private double ctrlWidth = 0;
        private double ctrlHeight = 0;

        // visible area on chart
        private double viewHeight = 0;
        private double viewWidth = 0;
        private double viewMax = 0;
        private double viewMin = 0;
        private double viewCenterY = 0;

        // highest and lowest point from data
        private double dataHigh;
        private double dataLow;
        private double dataLength;
        private double stepSizeVal;

        public double InitialScale => initialScale;
        public double DataLow => dataLow;
        public double DataHigh => dataHigh;
        public double ViewMax => viewMax;
        public double ViewMin => viewMin;

        // axis data
        int maxStepsOnYAxis = 20;
        double yAxisStepHeightPix = 0;
        int yAxisSteps = 0;

        public ScalableScaleCalculator()
        {
        }

        public void CalculateInitialScale(double ctrlW, double ctrlH, double dataL, double dataH, int dataLength)
        {
            if (ctrlH == 0 || ctrlW == 0) return;
            if (dataLength == 0) return;

            ctrlWidth = ctrlW;
            ctrlHeight = ctrlH;
            dataHigh = dataH;
            dataLow = dataL;
            this.dataLength = dataLength;

            // calculate view size
            viewMax = CalculatorHelpers.RoundToFirstPlus(dataHigh, 10);
            viewMin = CalculatorHelpers.RoundToFirstMininimum(dataLow, 10);
            viewHeight = viewMin < 0 ? viewMax + Math.Abs(viewMin) : viewMax - viewMin;
            viewCenterY = viewMin < 0 ? (viewMax + Math.Abs(viewMin)) / 2 : (viewMax - viewMin) / 2;

            initialScale = ctrlHeight / viewHeight;

            // calculate steps on Y axis
            //yAxisStepHeightPix = CalculatorHelpers.RoundToFirstPlus(viewHeight / maxStepsOnYAxis);
            //yAxisSteps = (int)((viewMax + Math.Abs(viewMin)) / yAxisStepHeightPix) + 1;
        }

        public void CalculateScale(double zoom)
        {
            this.zoom = zoom;
        }
    }
}
