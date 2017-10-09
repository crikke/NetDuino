using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;

namespace NetDuino.Models
{
    public interface IEntityValue<T>
    {
        T Value { get; set; }
    }

    public class SliderComponent : Component
    {
        public int MaxValue { get; set; }
        public int MinValue { get; set; }
    }

    public class ButtonComponent : Component
    {
        public bool IsToggleable { get; set; }
        public bool Toggle { get; set; }
    }

    public class LabelComponent : Component
    {   
    }

    public class Component : IDbEntry
    {
        public Component()
        {

        }
        public int Id { get; set; }
        public int Port { get; set; }
        public string Value { get; set; }
        public string ComponentName { get; set; }
        public DateTime LastUpdated { get; set; }

        public int ArduinoID { get; set; }
        public virtual ArduinoModel Arduino { get; set; }


    }
}