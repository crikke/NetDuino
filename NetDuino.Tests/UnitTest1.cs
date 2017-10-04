using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetDuino.Models;
using NetDuino.API;
using System.Web.Script.Serialization;
using NetDuino.Controllers;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using static NetDuino.API.ArduinoApiController;
using System.Collections.Generic;
using System.Linq;

namespace NetDuino.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestInitialize]
        public void Init()
        {
            TestMain.SetUser();
        }

        [TestMethod]
        public async Task UpdateComponent()
        {
            var context = new TestApplicationDbContext();
            ArduinoController controller = new ArduinoController(context, TestMain.SetUser());

            ArduinoApiController api = new ArduinoApiController(context);

            var Components = new List<ComponentModel>()
            {
                new ComponentModel() {
                    Port = 1,
                    ComponentName = "foo",
                    ArduinoID = 0,
                    Value = "0,11"
                }
            };

            var arduino = new ArduinoModel()
            {
                Name = "fiffi",
                UserId = "userId"
            };

            var authkey = controller.Create(arduino).Result.RouteValues["authkey"].ToString();
            await controller.AddComponent(new ArduinoViewModel() { Arduino = arduino, Component = Components.First() });

            var serialized = new JavaScriptSerializer().Serialize(Components);
            var responseMessage = api.UpdateComponent(authkey, serialized).Result;

            Assert.AreEqual(responseMessage.StatusCode, System.Net.HttpStatusCode.OK);
        }
    }
}