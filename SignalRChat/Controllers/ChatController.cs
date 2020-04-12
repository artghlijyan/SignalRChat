using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRChat.DbRepo;
using SignalRChat.Hubs;
using SignalRChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class ChatController : Controller
    {
        private IHubContext<ChatHub> _chat;

        public ChatController(IHubContext<ChatHub> chat)
        {
            _chat = chat;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> JoinRoom(string connId, string roomName)
        {
            await _chat.Groups.AddToGroupAsync(connId, roomName);
            return Ok();
        }

        [HttpPost("[action]/{connId}/{roomName}")]
        public async Task<IActionResult> LeaveRoom(string connId, string roomName)
        {
            await _chat.Groups.RemoveFromGroupAsync(connId, roomName);
            return Ok();
        }

        public async Task<IActionResult> SendMessage(
            int chatId,
            string message, 
            string roomName,
            [FromServices] AppDbContext dbContext)
        {
            var chatMessage = new Message()
            {
                ChatId = chatId,
                Text = message,
                Name = User.Identity.Name,
                TimeStamp = DateTime.Now
            };

            dbContext.Messages.Add(chatMessage);
            await dbContext.SaveChangesAsync();

            await _chat.Clients.Group(roomName).SendAsync("RecieveMessage", chatMessage);
            return Ok();
        }
    }
}
