using NetDuino.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace NetDuino.API
{
    public class ArduinoApiController : ApiController
    {
        private class DeserializedJson
        {
            public List<ComponentModel> Components { get; set; }
        }

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST api/<controller>/<authkey>
        public async Task<HttpResponseMessage> UpdateComponent(string authkey, [FromBody]string value)
        {
            var components = new JavaScriptSerializer().Deserialize<DeserializedJson>(value).Components;
            ArduinoModel arduino;

            using (var db = new ApplicationDbContext())
            {
                try
                {
                    arduino = db.Arduinos.Single(x => x.AuthKey == authkey);

                    foreach (var item in components)
                    {
                        var component = arduino.Components.Single(x => x.Port == item.Port);
                        component.Value = item.Value;
                        component.LastUpdated = DateTime.Now;
                    }
                    await db.SaveChangesAsync();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }
    }
}