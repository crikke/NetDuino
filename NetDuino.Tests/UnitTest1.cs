using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetDuino.Models;
using NetDuino.API;
using System.Web.Script.Serialization;
using NetDuino.Controllers;
using System.Threading.Tasks;

namespace NetDuino.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task UpdateComponent()
        {
            ArduinoController controller = new ArduinoController(new TestApplicationDbContext());
            ArduinoApiController api = new ArduinoApiController(new TestApplicationDbContext());
            var Component = new ComponentModel()
            {
                Port = 1,
                ComponentName = "foo",
                ArduinoID = 1,
                Value = "0,11"
            };

            var arduino = new ArduinoModel()
            {
                AuthKey = "foobar",
                Id = 1,
                Name = "fiffi",
                UserId = "guidguid"
            };

            await controller.Create(arduino);

            var serialized = new JavaScriptSerializer().Serialize(controller);

            var fii = api.UpdateComponent("foobar", serialized).Result;
        }
    }
}
