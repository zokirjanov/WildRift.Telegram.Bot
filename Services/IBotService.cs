using Telegram.Bot.Types;

namespace WildRift.Telegram.Bot.Services
{
	public interface IBotService
	{
		Task Test(string text);
	}
}
