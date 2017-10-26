using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NetDuino.Models;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

using static NetDuino.API.ArduinoApiController;

namespace NetDuino.Services
{
    public class ComponentServices
    {
        IApplicationDbContext ApplicationDbContext = new ApplicationDbContext();
        IHubContext hub = GlobalHost.ConnectionManager.GetHubContext<DashboardHub>();

        public ComponentServices()
        {
        }

        public ComponentServices(IApplicationDbContext dbContext)
        {
            ApplicationDbContext = dbContext;
        }

        public async Task UpdateComponentValue(string authkey, DeserializedValue deserializedValue)
        {
            try
            {
                var arduino = ApplicationDbContext.Arduinos.Single(x => x.AuthKey == authkey);
                var component = ApplicationDbContext.Components.Single(x => x.ArduinoID == arduino.Id && x.Port == deserializedValue.Port);

                component.LastUpdated = DateTime.Now;

                await ApplicationDbContext.SaveChangesAsync();

                return;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> AddComponent(Component component, string userId)
        {
            try
            {
                var arduino = ApplicationDbContext.Arduinos.Single(x => x.Id == component.ArduinoID);
                if (userId == arduino.UserId)
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

        public void AddValue(Component i)
        {
            ChartTick t = new ChartTick()
            {
                Time = DateTime.Now,
                Value = 0.22,
            };

            
            i.AddValue("Values", t);
        }
    }
}