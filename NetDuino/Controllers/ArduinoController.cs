using Microsoft.AspNet.Identity;
using NetDuino.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetDuino.Controllers
{
    public class ArduinoController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        ApplicationUserManager _userManager;

        public ArduinoController(ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }

        // GET: Arduino
        public ActionResult Index()
        {
            // display all current arduinos here.
            return View();
        }

        // GET: Arduino/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Arduino/Create
        [HttpPost]
        public  ActionResult Create(FormCollection collection)
        {
            try
            {
                if(User.Identity.IsAuthenticated)
                {
                    var model = new ArduinoModel()
                    {
                        AuthKey = Guid.NewGuid().ToString(),
                        User = _userManager.FindById(User.Identity.GetUserId()),
                        Name = collection["name"]
                    };

                    context.Arduinos.Add(model);
                    context.SaveChanges();
                    
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
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
