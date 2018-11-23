using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace RESTAURANT.API.App_Start
{
    public class NotificationHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
        private static IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();

        public void Hello(string message)
        {
            message = string.IsNullOrEmpty(message) ? "Hi :)" : message;
            Clients.All.hello(message);
        }
        public string callMe(string message)
        {
            Console.WriteLine(message);
            Clients.All.hello(message);
            return message;
        }
        public static void AddOrder(string client, string key)
        {
            hubContext.Clients.All.addOrder(client, key);
        }
    }
}