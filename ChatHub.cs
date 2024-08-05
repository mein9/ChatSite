using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using ChatSite.Data;
using ChatSite.Models;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

[Authorize] // Restrict access to authenticated users
public class ChatHub : Hub
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public ChatHub(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task SendMessage(string message)
    {
        var user = await _userManager.GetUserAsync(Context.User);
        var username = user?.UserName ?? "Anonymous";

        var chatMessage = new ChatMessage
        {
            User = username,
            Message = message,
            Timestamp = DateTime.UtcNow
        };

        _context.ChatMessages.Add(chatMessage);
        await _context.SaveChangesAsync();

        await Clients.All.SendAsync("ReceiveMessage", username, message);
    }
}