using Microsoft.EntityFrameworkCore;
using WildRift.Telegram.Bot.Models;

namespace WildRift.Telegram.Bot.DbContexts
{
	public class AppDbContext : DbContext
	{

		public DbSet<BuildItems> Items { get; set; }
		public DbSet<User> Users { get; set; }

		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

	}
}
