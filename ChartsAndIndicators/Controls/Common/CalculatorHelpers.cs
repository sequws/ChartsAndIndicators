using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Common
{
    static class CalculatorHelpers
    {
        #region helpers metod

        public static int CalcStepHeight(double min, double max, int maxSteps = 10)
        {
            int stepHeigh = 0;

            var cmax = RoundToFirstPlus(max);
            var cmin = RoundToFirstMinus(min);

            stepHeigh = RoundToFirstPlus((cmax + Math.Abs(cmin)) / maxSteps);

            return stepHeigh;
        }

        /// <summary>
        /// Returns first narest rounded to ten value
        /// </summary>
        public static int RoundToFirstPlus(double barMax, int roundedTo = 10)
        {
            var maxTenth = (int)(barMax / roundedTo) * roundedTo;
            if (barMax % roundedTo != 0) maxTenth += roundedTo;

            return maxTenth;
        }

        /// <summary>
        /// Returns minimal first rounded to ten value
        /// </summary>
        public static int RoundToFirstMinus(double barMin, int roundedTo = 10)
        {
            var minTenth = (int)(barMin / roundedTo) * roundedTo;
            if (barMin % roundedTo != 0) minTenth -= roundedTo;

            return minTenth;
        }

        #endregion
    }
}
