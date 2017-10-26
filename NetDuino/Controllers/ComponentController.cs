using Microsoft.AspNet.Identity;
using NetDuino.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace NetDuino.Controllers
{
    public class ComponentController : Controller
    {
        IApplicationDbContext dbContext = new ApplicationDbContext();

        public ComponentController() { }
        public ComponentController(IApplicationDbContext context)
        {
            dbContext = context;
        }

        public ActionResult GetComponentView(Component component)
        {
            ComponentDisplayAttribute propertyinfo = component.GetType().GetCustomAttributes(typeof(ComponentDisplayAttribute), true).FirstOrDefault() as ComponentDisplayAttribute;

            if (propertyinfo != null && !string.IsNullOrEmpty(propertyinfo.ViewURI))
            {
                return PartialView("~/Views/Shared/Components/" + propertyinfo.ViewURI + ".cshtml", component);
            }
            else
            {
                return PartialView("~/Views/Shared/Components/DefaultComponentPartial.cshtml", component);
            }
        }

        public ContentResult GetChartData(int id, int arduinoId)
        {
            var dashboard = dbContext.Arduinos.Single(x => x.Id == arduinoId);

            if (User.Identity.GetUserId() != dashboard.UserId)
                return Content("data : {}");

            var chart = dashboard.Components.Single(x => x.Id == id) as SimpleChartComponent;

            var chartData = chart.Values;

            if (chartData == null)
                return Content("data : {}");

            chartData.OrderBy(x => x.Time);
            
            var v = new List<ChartTick>()
            {
                new ChartTick() { Time = DateTime.Now, Value = 2.2},
                new ChartTick() { Time =  new DateTime(2017, 1, 11), Value = 12.1},
                new ChartTick() { Time =  new DateTime(2017, 2, 21), Value = 5.1},
                new ChartTick() { Time =  new DateTime(2017, 2, 24), Value = 1.1},
                new ChartTick() { Time =  new DateTime(2017, 3, 1), Value = 3.1},
                new ChartTick() { Time =  new DateTime(2017, 6, 2), Value = 14.1},
            };
            chart.Values = v;
            var value = new int[] { 10, 2 ,18,23, 5 }; ;
            var json = JsonConvert.SerializeObject(chart);
            //var value = chartData.Select(x => x.Value).ToList();
            return Content(json);
        }
    }
}