using Microsoft.AspNetCore.SignalR;

namespace LibraryMS.Api.Hubs;

// [Authorize]
public class NotificationHub : Hub
{
    public override Task OnConnectedAsync()
    {
        var userId = Context.UserIdentifier;
        return base.OnConnectedAsync();
    }
}