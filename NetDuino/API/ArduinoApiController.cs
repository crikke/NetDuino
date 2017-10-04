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
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST api/<controller>/<authkey>
        [HttpPost]
        public async Task<HttpResponseMessage> UpdateComponent(string authkey, [FromBody]string value)
        {
            var components = new JavaScriptSerializer().Deserialize<List<ComponentModel>>(value);
            ArduinoModel arduino;

            try
            {
                arduino = db.Arduinos.Where(x => x.AuthKey == authkey).FirstOrDefault();

                foreach (var item in components)
                {
                    var component = db.Components.Single(x => x.ArduinoID == arduino.Id && x.Port == item.Port);
                    component.Value = item.Value;
                    component.LastUpdated = DateTime.Now;
                }
                await db.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }
    }
}