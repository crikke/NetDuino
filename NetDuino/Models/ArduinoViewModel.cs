using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetDuino.Models
{
    public class ArduinoViewModel
    {
        public ArduinoModel Arduino { get; set; }
        public List<ComponentModel> Components { get; set; }
    }
}