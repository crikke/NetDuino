using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NetDuino.Models
{
    public class ArduinoComponentModel
    {
        public int ID { get; set; }
        // Port on the arduino
        public int Port { get; set; }
        // name for displaying on the interface
        public string ComponentName { get; set; }
        public string Value { get; set; }
        +public DateTime LastUpdated { get; set; }

        [Required]
        public int ArduinoID { get; set; }
        public virtual ArduinoModel Arduino { get; set; }
    }
}