using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using WildRift.Telegram.Bot.DbContexts;
using WildRift.Telegram.Bot.Interfaces;
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

		public async Task<Message> BotOnStartAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
		{
			try
			{


				ReplyKeyboardMarkup replyKeyboardMarkup = new(
					new[]
					{
						new KeyboardButton[] { "1.1", "1.2" },
						new KeyboardButton[] { "2.1", "2.2" },
					})
				{
					ResizeKeyboard = true
				};

				return await botClient.SendTextMessageAsync(
					chatId: message.Chat.Id,
					text: "Choose",
					replyMarkup: replyKeyboardMarkup,
					cancellationToken: cancellationToken);
			}
			catch (Exception)
			{
				return null;
			}
		}

		public async Task<Message> ItemImageInfoAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
		{
			try
			{
				await botClient.SendChatActionAsync(
					message.Chat.Id,
					ChatAction.UploadPhoto,
					cancellationToken: cancellationToken);

				const string filePath = "Files/Items/TrinityForce.jpg";
				await using FileStream fileStream = new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
				var fileName = filePath.Split(Path.DirectorySeparatorChar).Last();

				return await botClient.SendPhotoAsync(
					chatId: message.Chat.Id,
					replyToMessageId: message.ReplyToMessage.MessageId,
					photo: new InputFileStream(fileStream, fileName),
					cancellationToken: cancellationToken);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred: {ex.Message}");
				return null; 
			}
		}

		public async Task<Message> ItemInfoAsync(ITelegramBotClient _botClient, Message message, CancellationToken cancellationToken)
		{
			try
			{
				string stickerID = message.ReplyToMessage.Sticker.FileUniqueId;

				var sticker = await _dbContext.Items.FirstOrDefaultAsync(that => that.StickerId == stickerID);
				if (sticker == null) return null;

				string caption = $"{sticker.Name}\n\n" +
								 $"Stats: \n{sticker.Stats}\n\n" +
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

				return await _botClient.SendTextMessageAsync(
					chatId: message.Chat.Id,
					text: caption,
					replyMarkup: inlineKeyboard,
					replyToMessageId: message.ReplyToMessage.MessageId,
					cancellationToken: cancellationToken);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred: {ex.Message}");
				return null;
			}
		}
	}
}
