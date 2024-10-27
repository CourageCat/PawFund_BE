using MediatR;
using Microsoft.AspNetCore.SignalR;
using PawFund.Contract.Services.Messages;
using PawFund.Presentation.Abstractions;

namespace PawFund.Presentation.Hubs;

public class MessageHub : BaseHub
{
    public MessageHub(ISender sender) : base(sender)
    {
    }

    public async Task SendMessageWithStaffAsync(Command.CreateMesssageWithStaffCommand request)
    {
        var result = await Sender.Send(request);
        if (_userConnections.TryGetValue(result.Value.Data.ConnectionStaff, out string connectionUserId))
        {
            await Clients.Client(connectionUserId).SendAsync("onReceiveMessage", request);
        }
        if (_staffConnections.TryGetValue(result.Value.Data.ConnectionStaff, out string connectionStaffId))
        {
            await Clients.Client(connectionStaffId).SendAsync("onReceiveMessage", request);
        }
    }
}
