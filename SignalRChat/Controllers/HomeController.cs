using Microsoft.AspNetCore.Mvc;
using SignalRChat.DbRepo;
using SignalRChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _dbContext;

        public HomeController(AppDbContext dbContext) => _dbContext = dbContext;

        public IActionResult Index() => View();

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
