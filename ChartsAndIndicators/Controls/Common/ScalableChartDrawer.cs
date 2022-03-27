using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Controls.Common
{
    /// <summary>
    /// Allow draw chart which center can be moved and scaled
    /// </summary>
    public class ScalableChartDrawer
    {
        public double CenterX { get; set; }
        public double CenterY { get; set; }
        public double ViewHeight { get; set; }
        public double ViewWidth { get; set; }

        Canvas canvas;
    }
}
