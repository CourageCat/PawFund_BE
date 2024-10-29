using MediatR;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using PawFund.Contract.Abstractions.Services;
using PawFund.Contract.Services.Messages;
using PawFund.Presentation.Abstractions;

namespace PawFund.Presentation.Hubs;

public class MessageHub : BaseHub
{
    public MessageHub
        (ISender sender, IResponseCacheService responseCacheService)
        : base(sender, responseCacheService)
    {
    }

    public async Task SendMessageWithChatBotAsync(Command.CreateMessageWithChatBoxCommand request)
    {
        try
        {
            var result = await Sender.Send(request);
            var connectionUserIdMemory = await _responseCacheService.GetCacheResponseAsync($"memberConnection:{request.UserId.ToString()}");
            if (connectionUserIdMemory != null)
            {
                var connectionUserId = JsonConvert.DeserializeObject<string>(connectionUserIdMemory);
                await Clients.Client(connectionUserId).SendAsync("onReceiveMessageBot", result);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public async Task SendMessageWithStaffAsync(Command.CreateMesssageWithStaffCommand request)
    {
        try
        {
            var result = await Sender.Send(request);
            var connectionStaffIdMemory = await _responseCacheService.GetCacheResponseAsync($"staffConnection:{result.Value.Data.ReceiverId.ToString()}");
            if (connectionStaffIdMemory != null)
            {
                var connectionStaffId = JsonConvert.DeserializeObject<string>(connectionStaffIdMemory);
                await Clients.Client(connectionStaffId).SendAsync("onReceiveMessageUser", result);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public async Task SendMessageWithMemberAsync(Command.CreateMesssageWithUserCommand request)
    {
        try
        {
            var result = await Sender.Send(request);
            var connectionMemberIdMemory = await _responseCacheService.GetCacheResponseAsync($"memberConnection:{result.Value.Data.ReceiverId.ToString()}");
            if (connectionMemberIdMemory != null)
            {
                var connectionMemberId = JsonConvert.DeserializeObject<string>(connectionMemberIdMemory);
                await Clients.Client(connectionMemberId).SendAsync("onReceiveMessageUser", result);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public async Task GetListUserNeedSupport()
    {
        try
        {
            var result = await Sender.Send(new Query.GetListUserNeedSupportQuery());
            await Clients.Caller.SendAsync("onGetListUserNeedSupport", result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public async Task GetMessagesSenderIdAsync(Guid senderId)
    {
        try
        {
            var result = await Sender.Send(new Query.GetMessagesSenderIdQuery(senderId));
            await Clients.Caller.SendAsync("onGetMessagesSenderAsync", result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}
