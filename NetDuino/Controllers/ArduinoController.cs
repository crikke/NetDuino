using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using NetDuino.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NetDuino.Controllers
{
    public class ArduinoController : AsyncController
    {
        /// <summary>
        /// Application DB context
        /// </summary>
        protected ApplicationDbContext ApplicationDbContext { get; set; }

        /// <summary>
        /// User manager - attached to application DB context
        /// </summary>
        protected UserManager<ApplicationUser> UserManager { get; set; }

        public ArduinoController()
        {
            this.ApplicationDbContext = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.ApplicationDbContext));
        }

        // GET: Arduino
        public ActionResult Index()
        {
            // display all current arduinos here.
            return View();
        }

        // GET: Arduino/Display/{Name}
        public async Task<ActionResult> Display(string authkey)
        {
            using (var db = new ApplicationDbContext())
            {
                var duino = await db.Arduinos.Include(u => u.Components).SingleAsync(u => u.AuthKey == authkey);
                var components = db.Components.Where(u => u.ArduinoID == duino.ID).ToList();

                var viewModel = new ArduinoViewModel() { Arduino = duino };
                return View(viewModel);
            }
        }

        // GET: Arduino/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Arduino/Create
        [HttpPost]
        public async Task<ActionResult> Create(FormCollection collection)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var model = new ArduinoModel()
                    {
                        AuthKey = Helper.Helper.GenerateString(20),
                        UserId = User.Identity.GetUserId(),
                        Name = collection["name"]
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
            using (var db = new ApplicationDbContext())
            {
                if (User.Identity.GetUserId() == db.Arduinos.Single(x => model.ArduinoID == x.ID).UserId)
                {
                    db.Components.Add(model);
                    db.SaveChanges();
                }
            }
            return null;
        }
    }
}
