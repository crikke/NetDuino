using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using NetDuino.Models;
using NetDuino.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NetDuino.Controllers
{
    public class ArduinoController : AsyncController
    {
        IApplicationDbContext ApplicationDbContext = new ApplicationDbContext();
        ComponentServices componentServices = new ComponentServices();
        //new IPrincipal User { get; set; }

        public ArduinoController()
        {
            this.ApplicationDbContext = new ApplicationDbContext();
            //this.User = base.User;
        }

        public ArduinoController(IApplicationDbContext context, GenericPrincipal user)
        {
            ApplicationDbContext = context;
            componentServices = new ComponentServices(context);
            //User = new GenericPrincipal(null, null);
        }

        // GET: Arduino
        public async Task<ActionResult> Index()
        {
            // display all current arduinos here.
            var userID = User.Identity.GetUserId();
            var user = await HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindByIdAsync(userID);
            
            return View(user);
        }

        // GET: Arduino/Display/{Name}
        [Authorize]
        [Route("display/{authkey}")]
        public async Task<ActionResult> Display(string authkey)
        {
            var duino = await ApplicationDbContext.Arduinos.Include(u => u.Components).SingleAsync(u => u.AuthKey == authkey);

            var viewModel = new ArduinoViewModel() { Arduino = duino };
            return View(viewModel);
        }

        public ActionResult GetComponentMenu(int id)
        {
            ViewBag.Id = id;
            return PartialView("_ComponentMenuPartial", new ComponentViewModel());
        }

        // GET: Arduino/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Arduino/Create
        [Authorize]
        [HttpPost]
        public async Task<RedirectToRouteResult> Create(ArduinoModel collection)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var model = new ArduinoModel()
                    {
                        AuthKey = Helper.Helper.GenerateString(20),
                        UserId = User.Identity.GetUserId(),
                        Name = collection.Name
                    };

                    ApplicationDbContext.Arduinos.Add(model);
                    await ApplicationDbContext.SaveChangesAsync();

                    return RedirectToAction("Display", new { model.AuthKey });
                }

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<JsonResult> AddSlider(ComponentViewModel model)
        {
            if(await componentServices.AddComponent(model.Slider, User.Identity.GetUserId()))
            {
                return null;
            }
            return null;
        }

        [Authorize]
        [HttpPost]
        public async Task<JsonResult> AddLabel(ComponentViewModel model)
        {
            if (await componentServices.AddComponent(model.Label, User.Identity.GetUserId()))
            {
                return null;
            }
            return null; 
        }

        [Authorize]
        [HttpPost]
        public async Task<JsonResult> AddButton(ComponentViewModel model)
        {
            if (await componentServices.AddComponent(model.Button, User.Identity.GetUserId()))
            {
                return null;
            }
            return null;
        }

        [HttpPost]
        public async Task<ActionResult> UpdatePosition(PositionViewModel model)
        {
            try
            {
                int.TryParse(model.arduinoId, out var arduinoId);
                var arduino = ApplicationDbContext.Arduinos.Single(x => x.Id == arduinoId);

                var component = arduino.Components.Single(x => x.Id == int.Parse(model.elementId));

                component.Height = int.Parse(model.height);
                component.Width = int.Parse(model.width);
                component.PositionX = int.Parse(model.posX);
                component.PositionY = int.Parse(model.posY);

                await ApplicationDbContext.SaveChangesAsync();
                return null;
            }
            catch(Exception)
            {
                return new HttpStatusCodeResult(500, "internal server error");
            }
            
        }

        public class PositionViewModel
        {
            public string arduinoId { get; set; }
            public string elementId { get; set; }

            public string width { get; set; }
            public string height { get; set; }
            public string posX { get; set; }
            public string posY { get; set; }

        }
    }
}
