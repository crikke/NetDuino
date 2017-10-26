using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetDuino.Models
{
    public class ComponentViewModel
    {
        public ButtonComponent Button { get; set; }
        public LabelComponent Label { get; set; }
        public SliderComponent Slider { get; set; }
        public SimpleChartComponent Chart { get; set; }
    }
}