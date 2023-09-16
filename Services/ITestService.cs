using Telegram.Bot.Types;

namespace WildRift.Telegram.Bot.Services
{
	public interface ITestService
	{
		Task Test(string text);
	}
}
