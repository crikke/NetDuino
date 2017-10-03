using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NetDuino.Models
{
    public class ComponentModel : IDbEntry
    {
        public int Id { get; set; }
        // Port on the arduino
        public int Port { get; set; }
        // name for displaying on the interface
        public string ComponentName { get; set; }
        public string Value { get; set; }
        public DateTime LastUpdated { get; set; }

        public int ArduinoID { get; set; }
        public virtual ArduinoModel Arduino { get; set; }
    }
}