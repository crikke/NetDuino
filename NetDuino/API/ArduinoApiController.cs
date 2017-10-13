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
using NetDuino.Services;

namespace NetDuino.API
{
    public class ArduinoApiController : ApiController
    {
        IApplicationDbContext db = new ApplicationDbContext();
        ComponentServices componentServices = new ComponentServices();

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
                foreach (var item in components)
                {
                    await componentServices.UpdateComponentValue(authkey, item);
                }
            }
            catch (InvalidOperationException)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}