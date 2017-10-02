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
                var duino = await db.Arduinos.SingleAsync(u => u.AuthKey == authkey);
                var components = db.Components.Where(u => u.ArduinoID == duino.ID).ToList();

                var viewModel = new ArduinoViewModel() { Arduino = duino, Components = components };
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

        // GET: Arduino/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Arduino/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Arduino/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }



        // POST: Arduino/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
