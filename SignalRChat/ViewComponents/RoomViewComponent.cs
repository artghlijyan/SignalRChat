using Microsoft.AspNetCore.Mvc;
using SignalRChat.DbRepo;
using System.Linq;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace SignalRChat.ViewComponents
{
    public class RoomViewComponent : ViewComponent
    {
        private AppDbContext _dbContext;

        public RoomViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IViewComponentResult Invoke()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            
            var chats = _dbContext.
                ChatUsers.
                Include(x => x.Chat).
                Where(x => x.UserId == userId).
                Select(x => x.Chat).
                ToList();

            return View(chats);
        }
    }
}
