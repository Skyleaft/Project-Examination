using Microsoft.AspNetCore.SignalR;

namespace Web.Services.HubServices;

public class PresenceHub(OnlineUserService onlineUserService) : Hub
{
    public override async Task OnConnectedAsync()
    {
        var connectionId = Context.ConnectionId;
        var httpContext = Context.GetHttpContext();
        var name = Context.User?.Identity?.Name ?? httpContext?.Request.Query["user"].ToString();
        var ip = Context.GetHttpContext()?.Connection?.RemoteIpAddress?.ToString() ?? "Unknown";


        var device = httpContext?.Request.Query["device"].ToString();
        var latitude = httpContext?.Request.Query["lat"].ToString();
        var longitude = httpContext?.Request.Query["lng"].ToString();

        var user = new OnlineUser
        {
            Id = connectionId,
            Name = name,
            IpAddress = ip,
            Device = device,
            Latitude = double.Parse(latitude),
            Longitude = double.Parse(longitude)
        };

        onlineUserService.AddUser(connectionId, user);

        await Clients.All.SendAsync("UserListUpdated", onlineUserService.GetAllUsers());
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        onlineUserService.RemoveUser(Context.ConnectionId);

        await Clients.All.SendAsync("UserListUpdated", onlineUserService.GetAllUsers());
    }
}