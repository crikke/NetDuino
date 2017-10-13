using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using NetDuino.Models;
using System.Threading.Tasks;

namespace NetDuino.Services
{
    public class DashboardHub : Hub
    {
        public void UpdateComponent(string authkey, Component component)
        {
            this.Clients.Group(authkey).onUpdateComponent(component);
        }

        public Task JoinRoom(string authkey)
        {
            return Groups.Add(Context.ConnectionId, authkey);
        }

        public Task LeaveRoom(string authkey)
        {
            return Groups.Remove(Context.ConnectionId, authkey);
        }
    }
}