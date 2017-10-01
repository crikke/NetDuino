using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NetDuino.Models
{

    // need a URI 

    public class ArduinoModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string AuthKey { get; set; }

        public ICollection<ComponentModel> Components { get; set; }

        [Required]
        public int UserID { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}