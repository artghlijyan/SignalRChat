using Microsoft.AspNetCore.Mvc;
using SignalRChat.DbRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            var chats = _dbContext.Chats.ToList();
            return View(chats);
        }
    }
}
