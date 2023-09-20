using WildRift.Telegram.Bot.DbContexts;
using WildRift.Telegram.Bot.Models;

namespace WildRift.Telegram.Bot.Services
{
	public class BotService : IBotService
	{
		private readonly AppDbContext _dbContext;

		public BotService(AppDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task Test(string text)
		{
			BuildItems item = new BuildItems()
			{
				Name = text,
			};

			 _dbContext.Items.Add(item);
			int x = _dbContext.SaveChanges();

			int s = 3;
			return;
		}
	}
}
