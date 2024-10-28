using MediatR;
using Microsoft.AspNetCore.SignalR;
using PawFund.Contract.Abstractions.Services;
using PawFund.Contract.Enumarations.Authentication;

namespace PawFund.Presentation.Abstractions;

public abstract class BaseHub : Hub
{
    protected readonly ISender Sender;
    protected readonly IResponseCacheService _responseCacheService;

    protected BaseHub(ISender sender, IResponseCacheService responseCacheService)
    {
        Sender = sender;
        _responseCacheService = responseCacheService;
    }

    public override async Task OnConnectedAsync()
    {
        try
        {
            var userId = Guid.Parse(Context.GetHttpContext().Request.Query["userId"]);
            var roleId = Int32.Parse(Context.GetHttpContext().Request.Query["role"]);
            if (userId != null)
            {
                if (roleId == (int)RoleType.Member)
                    await _responseCacheService.SetCacheResponseNoTimeoutAsync($"memberConnection:{userId}", Context.ConnectionId);
                if (roleId == (int)RoleType.Staff)
                    await _responseCacheService.SetCacheResponseNoTimeoutAsync($"staffConnection:{userId}", Context.ConnectionId);
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

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        var userId = Guid.Parse(Context.GetHttpContext().Request.Query["userId"]);
        var roleId = int.Parse(Context.GetHttpContext().Request.Query["role"]);

        if (roleId == (int)RoleType.Member)
            await _responseCacheService.DeleteCacheResponseAsync($"memberConnection:{userId}");
        if (roleId == (int)RoleType.Staff)
            await _responseCacheService.DeleteCacheResponseAsync($"staffConnection{userId}");

        await base.OnDisconnectedAsync(exception);
    }
}
