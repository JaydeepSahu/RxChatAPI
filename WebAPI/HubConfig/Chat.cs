using Microsoft.AspNetCore.SignalR;
using WebAPI.HubModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainLayer.Models;

namespace WebAPI.HubConfig
{
    //4Tutorial
    public partial class MyHub
    {
        public async Task getOnlineUsers()
        {
            Guid currUserId = ctx.Connections.Where(c => c.SignalrId == Context.ConnectionId).Select(c => c.PersonId).SingleOrDefault();
            List<User> onlineUsers = ctx.Connections
                .Where(c => c.PersonId != currUserId)
                .Select(c =>
                    new User(c.PersonId, ctx.Person.Where(p => p.Id == c.PersonId).Select(p => p.Name).SingleOrDefault(), c.SignalrId)
                ).ToList();
            await Clients.Caller.SendAsync("getOnlineUsersResponse", onlineUsers);
        }


        public async Task sendMsg(string connId, string msg)
        {

            Guid fromUserId = ctx.Connections.Where(c => c.SignalrId == Context.ConnectionId).Select(c => c.PersonId).SingleOrDefault();
            Guid toUserId = ctx.Connections.Where(c => c.SignalrId == connId).Select(c => c.PersonId).SingleOrDefault();
            UserChat txtMsg = new UserChat
            {
                From = fromUserId.ToString(),
                To = toUserId.ToString(),
                Message = msg,
                AddedOn = DateTime.Now,
                IsActive = true
            };
            await ctx.UserChat.AddAsync(txtMsg);
            await ctx.SaveChangesAsync();
            await Clients.Client(connId).SendAsync("sendMsgResponse", Context.ConnectionId, msg);
        }


    }
}
