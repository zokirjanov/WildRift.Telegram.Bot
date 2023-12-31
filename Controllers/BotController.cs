﻿using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using WildRift.Telegram.Bot.Filters;
using WildRift.Telegram.Bot.Services;

namespace WildRift.Telegram.Bot.Controllers
{
	public class BotController : ControllerBase
	{
		[HttpPost]
		[ValidateTelegramBot]
		public async Task<IActionResult> Post(
		[FromBody] Update update,
		[FromServices] UpdateHandlers handleUpdateService,
		CancellationToken cancellationToken)
		{
			await handleUpdateService.HandleUpdateAsync(update, cancellationToken);
			return Ok();
		}
	}
}
