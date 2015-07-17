using System;
using System.Runtime.Remoting.Contexts;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace ReviewMe.Hubs
{
    public class ReviewMeHub : Hub
    {
        public static IHubCallerConnectionContext<dynamic> GroupClients;

        public void Send(string userId)
        {
            GroupClients = Clients;
            Groups.Add(Context.ConnectionId, userId);

            //Clients.All.broadcastMessage(userId, taskId);
        }

        public void NotificationBroadCastToUser(int userId, string message)
        {
            if (GroupClients == null)
                GroupClients = Clients;
            GroupClients.Group(Convert.ToString(userId)).broadcastMessage(message);


        }

        /// <summary>
        /// used to send notification to client for Notification Bar
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="message"></param>
        public void SendNotificationTop(long userId, string path, long notifId, string message)
        {
            GroupClients.Group(Convert.ToString(userId)).notificationMessage(path, notifId, message);
        }

        public void SendNotifications(string message)
        {
            Clients.All.receiveNotification(message);
        }
    }
}
