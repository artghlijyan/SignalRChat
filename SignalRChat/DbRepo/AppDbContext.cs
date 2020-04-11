using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SignalRChat.Models;

namespace SignalRChat.DbRepo
{
    public class SigninManager : IdentityDbContext<User>
    {
        public SigninManager(DbContextOptions<SigninManager> options) : base(options) { }
        
        public DbSet<Chat> Chats { get; set; }

        public DbSet<Message> Messages { get; set; }

    }
}
