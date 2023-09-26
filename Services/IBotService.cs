using Telegram.Bot;
using Telegram.Bot.Types;

namespace WildRift.Telegram.Bot.Services
{
	public interface IBotService
	{
		Task ItemInfoAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken);
		Task ItemImageInfoAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken);
	}
}
