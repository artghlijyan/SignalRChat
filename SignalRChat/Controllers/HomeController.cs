using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalRChat.DbRepo;
using SignalRChat.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private SigninManager _dbContext;

        public HomeController(SigninManager dbContext) => _dbContext = dbContext;

        public IActionResult Index() => View();

        [HttpGet("{id}")]
        public IActionResult Chat(int id)
        {
            var chat = _dbContext.
                Chats.
                Include(x => x.Messages).
                FirstOrDefault(x => x.Id == id);
            return View(chat);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(int chatId, string message)
        {
            var chatMessage = new Message()
            {
                ChatId = chatId,
                Text = message,
                Name = User.Identity.Name,
                TimeStamp = DateTime.Now
            };

            _dbContext.Messages.Add(chatMessage);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Chat", new { id = chatId });
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom(string name)
        {
            _dbContext.Add(new Chat
            {
                Name = name,
                Type = ChatType.Room
            });

            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
