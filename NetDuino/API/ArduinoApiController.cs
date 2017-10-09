using NetDuino.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Data.Entity;
using System.Web.Script.Serialization;

namespace NetDuino.API
{
    public class ArduinoApiController : ApiController
    {
        IApplicationDbContext db = new ApplicationDbContext();

        public ArduinoApiController() { }

        public ArduinoApiController(IApplicationDbContext context)
        {
            db = context;
        }
        // GET api/<controller>

        public class DeserializedValue
        {
            public int Port { get; set; }
            public string Value { get; set; }
        }

        // POST api/<controller>/<authkey>
        [HttpPost]
        public async Task<HttpResponseMessage> UpdateComponentValue(string authkey, [FromBody]string value)
        {
            var components = new JavaScriptSerializer().Deserialize<List<DeserializedValue>>(value);

            try
            {
                var arduino = db.Arduinos.Single(x => x.AuthKey == authkey);

                foreach (var item in components)
                {
                    var component = db.Components.Single(x => x.ArduinoID == arduino.Id && x.Port == item.Port);
                    component.LastUpdated = DateTime.Now;

                    component.Value = item.Value;

                }
                await db.SaveChangesAsync();
            }
            catch (InvalidOperationException)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}