using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using ChatSite.Data;
using ChatSite.Models;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

[Authorize] // Restrict access to authenticated users
public class ChatHub : Hub
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ILogger<ChatHub> _logger;

    public ChatHub(ApplicationDbContext context, UserManager<IdentityUser> userManager, ILogger<ChatHub> logger)
    {
        _context = context;
        _userManager = userManager;
        _logger = logger;
    }

    public async Task SendMessage(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            _logger.LogWarning("Attempted to send an empty message.");
            return;
        }

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

        _logger.LogInformation("Message sent by {Username}: {Message}", username, message);

        await Clients.All.SendAsync("ReceiveMessage", username, message);
    }
}