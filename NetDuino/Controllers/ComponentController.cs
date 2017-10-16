using NetDuino.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NetDuino.Controllers
{
    public class ComponentController : Controller
    {
        public ActionResult GetComponentView(Component component)
        {
            ComponentDisplayAttribute propertyinfo = (ComponentDisplayAttribute)Attribute.GetCustomAttributes(component.GetType()).FirstOrDefault();

            if (propertyinfo != null && !string.IsNullOrEmpty(propertyinfo.ViewURI))
            {
                return PartialView("~/Views/Shared/Components/" + propertyinfo.ViewURI + ".cshtml", component);
            }
            else
            {
                return PartialView("~/Views/Shared/Components/DefaultComponentPartial.cshtml", component);
            }
        }
    }
}