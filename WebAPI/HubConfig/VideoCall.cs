using DomainLayer.Models;
using Microsoft.AspNetCore.SignalR;

namespace WebAPI.HubConfig
{
    public partial class MyHub
    {
        public async Task sendVideoCall(string connId)
        {
            await Clients.Client(connId).SendAsync("sendVideoCallResponse", connId, Context.ConnectionId);
        }

        public async Task VideoCallAccepted(string connId)
        {
            await Clients.Client(connId).SendAsync("VideoCallAcceptedResponse", connId);
        }
    }
}
