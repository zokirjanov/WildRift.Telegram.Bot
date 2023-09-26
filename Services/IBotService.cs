using Telegram.Bot;
using Telegram.Bot.Types;

namespace WildRift.Telegram.Bot.Services
{
	public interface IBotService
	{
	    Task<Message> ItemInfoAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken);
		Task<Message> ItemImageInfoAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken);
	}
}
