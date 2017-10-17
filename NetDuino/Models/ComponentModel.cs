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
    [AttributeUsage(AttributeTargets.Property)]
    public class ComponentPropertyAttribute : Attribute
    {
        public bool CanEdit { get; set; }
        public string DisplayName { get; set; }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class ComponentDisplayAttribute : Attribute
    {
        public string ViewURI { get; set; }
    }

    [ComponentDisplay(ViewURI = "SliderComponentPartial")]
    public class SliderComponent : Component
    {
        [ComponentProperty(CanEdit = true, DisplayName ="Max slider value")]
        public int MaxValue { get; set; }

        [ComponentProperty(CanEdit = true)]
        public int MinValue { get; set; }

        [ComponentProperty(CanEdit = true)]
        public int SliderValue { get; set; }
    }

    public class ButtonComponent : Component
    {
        [ComponentProperty(CanEdit = true)]
        public bool IsToggle { get; set; }

        [ComponentProperty(CanEdit = true)]
        public int ToggleValue { get; set; }
    }

    public class LabelComponent : Component
    {
        [ComponentProperty(CanEdit = true)]
        public string LabelValue { get; set; }
    }

    public abstract class Component : IDbEntry
    {
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

        public Dictionary<string, object> GetValues()
        {
            var res = new Dictionary<string, object>();

            var properties = this.GetType().GetProperties();
            foreach (var item in properties)
            {
                var propertyInfo = item.GetCustomAttribute<ComponentPropertyAttribute>();
                if (propertyInfo != null && propertyInfo.CanEdit)
                {
                    var displayname = propertyInfo.DisplayName ?? item.Name;
                    res.Add(displayname, item.GetValue(this));
                }
            }
            return res;
        }

        public object GetValue(string key)
        {
            var properties = this.GetType().GetProperties();
            foreach (var item in properties)
            {
                var propertyInfo = item.GetCustomAttribute<ComponentPropertyAttribute>();
                if(propertyInfo != null && propertyInfo.CanEdit)
                {
                    if (item.Name == key)
                        return item.GetValue(this);
                }
            }
            return null;
        }

        public bool SetValue(string key, object val)
        {
            // if i would use reflection, make custom attribute what properties can be changed
            var property = GetType().GetProperties().Single(x => x.Name == key);
            var propertyInfo = property.GetCustomAttribute<ComponentPropertyAttribute>();

            if (propertyInfo.CanEdit)
            {
                property.SetValue(this, val);
                return true;
            }
            return false;
        }
    }
}