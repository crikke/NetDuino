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
    public class SliderComponent : Component
    {
        public int MaxValue { get; set; }
        public int MinValue { get; set; }

        public int SliderValue { get; set; }

        public override Dictionary<string, string> GetValues()
        {
            var res = new Dictionary<string, string>
            {
                { "SliderValue", SliderValue.ToString() }
            };

            return res;
        }
    }

    public class ButtonComponent : Component
    {
        public bool IsToggle { get; set; }

        public int ToggleValue { get; set; }

        public override Dictionary<string, string> GetValues()
        {
            var res = new Dictionary<string, string>();
            res.Add("IsToggle", "true");
            res.Add("ToggleValue", ToggleValue.ToString());

            return res;
        }
    }

    public class LabelComponent : Component
    {
        public string LabelValue { get; set; }

        public override Dictionary<string, string> GetValues()
        {
            var res = new Dictionary<string, string>();
            res.Add("Label", LabelValue);

            return res;
        }
    }

    public abstract class Component : IDbEntry
    {

        public Component()
        {

        }

        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Id { get; set; }
        public int Port { get; set; }
        public string ComponentName { get; set; }
        public DateTime LastUpdated { get; set; }

        public int ArduinoID { get; set; }
        public virtual ArduinoModel Arduino { get; set; }

        public abstract Dictionary<string, string> GetValues();
    }
}