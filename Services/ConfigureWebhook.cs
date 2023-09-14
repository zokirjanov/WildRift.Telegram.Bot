using Microsoft.Extensions.Options;
using Telegram.Bot.Types.Enums;
using Telegram.Bot;

namespace WildRift.Telegram.Bot.Services
{
	public class ConfigureWebhook : IHostedService
	{
		private readonly ILogger<ConfigureWebhook> _logger;
		private readonly IServiceProvider _serviceProvider;
		private readonly BotConfiguration _botConfig;

        public ConfigureWebhook(
		ILogger<ConfigureWebhook> logger,
		IServiceProvider serviceProvider,
		IOptions<BotConfiguration> botOptions)
		{
			_logger = logger;
			_serviceProvider = serviceProvider;
			_botConfig = botOptions.Value;
		}

        public  async Task StartAsync(CancellationToken cancellationToken)
		{
			using var scope = _serviceProvider.CreateScope();
			var botClient = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();

			//var webhookAddress = $"{_botConfig.HostAddress}{_botConfig.Route}";
			var webhookAddress = $"https://api.telegram.org/bot6598757041:AAFAAOc0CBkk7f1lhqHItJYEm4bCpA0uxFE/setwebhook?url=https://7743-91-90-219-121.ngrok.io";
			_logger.LogInformation("Setting webhook: {https://be85-91-90-219-121.ngrok.io}", webhookAddress);
			await botClient.SetWebhookAsync(
				url: webhookAddress,
				allowedUpdates: Array.Empty<UpdateType>(),
				secretToken: _botConfig.SecretToken,
				cancellationToken: cancellationToken);
		}

		public async Task StopAsync(CancellationToken cancellationToken)
		{
			using var scope = _serviceProvider.CreateScope();
			var botClient = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();

			_logger.LogInformation("Removing webhook");
			await botClient.DeleteWebhookAsync(cancellationToken: cancellationToken);
		}
	}
}
