using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NetDuino.Models;
using System.Threading.Tasks;

namespace NetDuino.Services
{
    public class ComponentServices
    {
        IApplicationDbContext ApplicationDbContext = new ApplicationDbContext();

        public ComponentServices()
        {

        }

        public ComponentServices(IApplicationDbContext dbContext)
        {
            ApplicationDbContext = dbContext;
        }

        public async Task<bool> AddComponent(Component component, string userId)
        {
            try
            {
                if (userId == ApplicationDbContext.Arduinos.Single(x => x.Id == component.ArduinoID).UserId)
                {
                    component.LastUpdated = DateTime.Now;

                    ApplicationDbContext.Components.Add(component);
                    await ApplicationDbContext.SaveChangesAsync();

                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}