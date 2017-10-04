using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using NetDuino.Models;
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
        new IPrincipal User { get; set; }

        public ArduinoController()
        {
            this.ApplicationDbContext = new ApplicationDbContext();
            this.User = base.User;
        }

        public ArduinoController(IApplicationDbContext context, IPrincipal user)
        {
            ApplicationDbContext = context;
            User = user;
        }

        // GET: Arduino
        public ActionResult Index()
        {
            // display all current arduinos here.
            return View();
        }

        // GET: Arduino/Display/{Name}
        [Authorize]
        public async Task<ActionResult> Display(string authkey)
        {
            var duino = await ApplicationDbContext.Arduinos.Include(u => u.Components).SingleAsync(u => u.AuthKey == authkey);
            var components = ApplicationDbContext.Components.Where(u => u.ArduinoID == duino.Id).ToList();

            var viewModel = new ArduinoViewModel() { Arduino = duino };
            return View(viewModel);
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
        public async Task<JsonResult> AddComponent(ArduinoViewModel collection)
        {
            var model = new ComponentModel()
            {
                ArduinoID = collection.Component.ArduinoID,
                ComponentName = collection.Component.ComponentName,
                Port = collection.Component.Port,
                LastUpdated = DateTime.Now
            };

            if (User.Identity.GetUserId() == ApplicationDbContext.Arduinos.Single(x => model.ArduinoID == x.Id).UserId)
            {
                ApplicationDbContext.Components.Add(model);
                await ApplicationDbContext.SaveChangesAsync();
            }

            return null;
        }
    }
}
