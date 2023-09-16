using WildRift.Telegram.Bot.DbContexts;
using WildRift.Telegram.Bot.Models;

namespace WildRift.Telegram.Bot.Services
{
	public class TestClsss : ITestService
	{
		private readonly AppDbContext _dbContext;

		public TestClsss(AppDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task Test(string text)
		{
			BuildItems item = new BuildItems()
			{
				Id = 1,
				StickerId = 1,
				Name = text,
			};

			_dbContext.Items.Add(item);
			int x = _dbContext.SaveChanges();

			int s = 3;
			return;
		}
	}
}
