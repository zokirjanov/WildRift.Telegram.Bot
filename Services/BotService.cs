using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using WildRift.Telegram.Bot.DbContexts;
using WildRift.Telegram.Bot.Models;

namespace WildRift.Telegram.Bot.Services
{
	public class BotService : IBotService
	{
		private readonly AppDbContext _dbContext;
		private readonly ILogger<UpdateHandlers> _logger;

		public BotService(AppDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task ItemInfoAsync(ITelegramBotClient _botClient, Message message, CancellationToken cancellationToken)
		{
			string stickerID = message.ReplyToMessage.Sticker.FileId;

			try
			{
				var sticker = await _dbContext.Items.FirstOrDefaultAsync(that => that.StickerId == stickerID);

				string caption = $"{sticker.Name}\n\n" +
								 $"Stats: \n{sticker.Stats}\n\n" +
								 $"Passive: {sticker.Passive}\n\n" +
								 $"Patch: {sticker.Patch}";

				InlineKeyboardMarkup inlineKeyboard = new(
					new[]
					{
                    new []
					{
						InlineKeyboardButton.WithCallbackData("🇷🇺 Ru", "ru"),
						InlineKeyboardButton.WithCallbackData("🇺🇿 Uz", "uz"),
					},
					});

				await  _botClient.SendTextMessageAsync(
					chatId: message.Chat.Id,
					text: caption,
					replyMarkup: inlineKeyboard,
					replyToMessageId: message.ReplyToMessage.MessageId,
					cancellationToken: cancellationToken);
			}
			catch (Exception)
			{
				_logger.LogInformation("Receive message type: {MessageType}", message.Type);
			}
			throw new NotImplementedException();
		}
	}
}
