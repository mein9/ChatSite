using Microsoft.AspNetCore.Mvc;
using ChatSite.Data;
using ChatSite.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

[Authorize] // Add this line to restrict access to authenticated users
public class ChatController : Controller
{
    private readonly ApplicationDbContext _context;

    public ChatController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var messages = await _context.ChatMessages
            .OrderBy(m => m.Timestamp)
            .ToListAsync();

        return View(messages);
    }
}