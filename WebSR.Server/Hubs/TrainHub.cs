using Microsoft.AspNetCore.SignalR;
using SR.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSR.Server
{
    public class TrainHub : Hub
    {
        public override Task OnConnectedAsync()
        {          
            return base.OnConnectedAsync();
        }

        public Task JoinRoom(string roomName)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, roomName);
        }

        public Task RemoveRoom(string roomName)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        //public async Task SendMessage(string user, string message)
        //{
        //    await Clients.All.SendAsync("ReceiveMessage", user, message);
        //}

        public Task SendMaterieelvirtueel(MaterieelVirtueel train)
        {            
            return Clients.All.SendAsync("ReceiveTrain", train);
        }
    } 

}
