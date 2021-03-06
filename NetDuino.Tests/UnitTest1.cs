﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetDuino.Models;
using System.Data.Entity;
using NetDuino.API;
using System.Web.Script.Serialization;
using NetDuino.Controllers;
using System.Security.Principal;
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
        public void AddValueToChart()
        {
            var user = TestMain.SetUser();
            var context = new TestApplicationDbContext();
            var controller = new DashboardController(context, null);


             context.Arduinos.Add(new ArduinoModel()
            {
                Id = 1,
                Name = "Test",
                UserId = "0",
                AuthKey = "auth"
            });

            var c = new SimpleChartComponent()
            {
                ArduinoID = 1,
                ComponentName = "Chart",
                Height = 1,
                Width = 1,
                XLabel = "X Label",
                YLabel = "Y Label",
                LastUpdated = DateTime.Now,
                Id = 0,
                Port = 0,
                Values = new List<ChartTick>(),
                
            };
            c.AddValue("Values", new ChartTick() { ChartId = 0, Id = 0, Time = DateTime.Now, Value = 2.23 });
        }


        //[TestMethod]
        //public async Task UpdateComponent()
        //{
        //    var context = new TestApplicationDbContext();
        //    ArduinoController controller = new ArduinoController(context, TestMain.SetUser());
        //    ArduinoApiController api = new ArduinoApiController(context);

        //    var Components = new List<Component>()
        //    {
        //        new Component() {
        //            Port = 1,
        //            ComponentName = "foo",
        //            ArduinoID = 0,
        //            Value = "1"
        //        }
        //    };

        //    var arduino = new ArduinoModel()
        //    {
        //        Name = "fiffi",
        //        UserId = "userId"
        //    };

        //    var authkey = controller.Create(arduino).Result.RouteValues["authkey"].ToString();
        //    //await controller.AddComponent(new ArduinoViewModel() { Arduino = arduino, Components = Components.First() });

        //    var componentToUpdate = new List<ArduinoApiController.DeserializedValue>()
        //    {
        //        new DeserializedValue
        //        {
        //            Port = 1,
        //            Value = "2",
        //        }
        //    };

        //    var serialized = new JavaScriptSerializer().Serialize(componentToUpdate);
        //    var responseMessage = api.UpdateComponentValue(authkey, serialized).Result;

        //    Assert.AreEqual(responseMessage.StatusCode, System.Net.HttpStatusCode.OK);
        //}
    }
}