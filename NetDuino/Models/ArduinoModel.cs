using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NetDuino.Models
{

    // need a URI 

    public class ArduinoModel : IDbEntry
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AuthKey { get; set; }

        public virtual ICollection<Component> Components { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}