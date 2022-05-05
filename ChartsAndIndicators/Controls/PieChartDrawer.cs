﻿using Controls.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Controls
{
    public class PieChartDrawer
    {
        public double ChartMargin { get; set; } = 20;


        Canvas canvas;

        public PieChartDrawer(Canvas canvas)
        {
            this.canvas = canvas;
        }

        public void Draw(List<PiePart> data)
        {
            if (data.Count == 0) return;

            var sum = data.Sum(x => x.Value);
        }
    }
}
