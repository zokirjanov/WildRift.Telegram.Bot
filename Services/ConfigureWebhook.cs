using Microsoft.Extensions.Options;
using Telegram.Bot.Types.Enums;
using Telegram.Bot;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WildRift.Telegram.Bot.DbContexts;

namespace WildRift.Telegram.Bot.Services
{
	public class ConfigureWebhook : IHostedService
	{
		private readonly ILogger<ConfigureWebhook> _logger;
		private readonly IServiceProvider _serviceProvider;
		private readonly BotConfiguration _botConfig;
		private readonly IConfiguration _configuration;

		public ConfigureWebhook(
		ILogger<ConfigureWebhook> logger,
		IServiceProvider serviceProvider,
		IConfiguration configuration,
		IOptions<BotConfiguration> botOptions)
		{
			_logger = logger;
			_serviceProvider = serviceProvider;
			_botConfig = botOptions.Value;
			_configuration = configuration;
		}

        public async Task StartAsync(CancellationToken cancellationToken)
		{
			using var scope = _serviceProvider.CreateScope();
			var botClient = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();

			var webhookAddress = $"{_botConfig.HostAddress}{_botConfig.Route}";
			_logger.LogInformation("Setting webhook: {WebhookAddress}", webhookAddress);
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

		public void ConfigureServices(IServiceCollection services)
		{
			string connectionString = _configuration.GetConnectionString("connectionString");

			var serverVersion = new MySqlServerVersion(new Version(8, 0, 11));

			services.AddDbContext<AppDbContext>(options =>
				options.UseMySql(connectionString, serverVersion));
		}
	}
}
