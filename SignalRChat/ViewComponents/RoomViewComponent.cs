using Microsoft.AspNetCore.Mvc;
using SignalRChat.DbRepo;
using System.Linq;

namespace SignalRChat.ViewComponents
{
    public class RoomViewComponent : ViewComponent
    {
        private SigninManager _dbContext;

        public RoomViewComponent(SigninManager dbContext)
        {
            _dbContext = dbContext;
        }

        public IViewComponentResult Invoke()
        {
            var chats = _dbContext.Chats.ToList();
            return View(chats);
        }
    }
}
