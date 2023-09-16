using Microsoft.EntityFrameworkCore;
using WildRift.Telegram.Bot.Models;

namespace WildRift.Telegram.Bot.DbContexts
{
	public class AppDbContext : DbContext
	{

		DbSet<BuildItems> Items { get; set; }

		public AppDbContext(DbContextOptions<AppDbContext> options)
		: base(options)
		{

		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			optionsBuilder.UseMySql("Server=localhost;Port=3306;User ID=root;Password=azeazease;database=wildrift;" +
			"Persist Security Info=false;Charset=utf8_general_ci", new MySqlServerVersion(new Version(8, 0, 11)));
		}
	}
}
