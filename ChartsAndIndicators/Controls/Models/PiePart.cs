using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Controls.Models
{
    public class PiePart
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
        public Brush Color { get; set; }

        public PiePart(int id, string desc, double value, Brush color)
        {
            Id = id;
            Description = desc;
            Value = value;
            Color = color;
        }
    }
}
