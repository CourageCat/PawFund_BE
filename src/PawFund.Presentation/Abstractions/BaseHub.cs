using MediatR;
using Microsoft.AspNetCore.SignalR;
using PawFund.Contract.Enumarations.Authentication;
using System.Collections.Concurrent;

namespace PawFund.Presentation.Abstractions;

public abstract class BaseHub : Hub
{
    protected readonly ISender Sender;
    protected readonly ConcurrentDictionary<Guid, string> _userConnections = new();
    protected readonly ConcurrentDictionary<Guid, string> _staffConnections = new();

    protected BaseHub(ISender sender)
    => Sender = sender;

    public override async Task OnConnectedAsync()
    {
        try
        {
            var userId = Guid.Parse(Context.GetHttpContext().Request.Query["userId"]);
            var roleId = int.Parse(Context.GetHttpContext().Request.Query["roleId"]);
            if (userId != null)
            {
                if(roleId == (int)RoleType.Member)
                    _userConnections[userId] = Context.ConnectionId;
                if(roleId == (int)RoleType.Staff) 
                    _staffConnections[userId] = Context.ConnectionId;
                await Clients.Caller.SendAsync("onSuccess", "Successfully connected.");
            }
            else
            {
                await Clients.Caller.SendAsync("onError", "User ID not found.");
            }
            Console.WriteLine(userId);
        }
        catch (Exception ex)
        {
            await Clients.Caller.SendAsync("onError", $"message: {ex.Message}");
        }

        await base.OnConnectedAsync();
    }
}
