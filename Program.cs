using Telegram.Bot;
using WildRift.Telegram.Bot;
using WildRift.Telegram.Bot.Controllers;
using WildRift.Telegram.Bot.Services;

var builder = WebApplication.CreateBuilder(args);

var botConfigurationSection = builder.Configuration.GetSection(BotConfiguration.Configuration);
builder.Services.Configure<BotConfiguration>(botConfigurationSection);

var botConfiguration = botConfigurationSection.Get<BotConfiguration>();



builder.Services.AddHttpClient("telegram_bot_client")
				.AddTypedClient<ITelegramBotClient>((httpClient, sp) =>
				{
					BotConfiguration? botConfig = sp.GetConfiguration<BotConfiguration>();
					TelegramBotClientOptions options = new(botConfig.BotToken);
					return new TelegramBotClient(options, httpClient);
				});

builder.Services.AddScoped<UpdateHandlers>();

builder.Services.AddHostedService<ConfigureWebhook>();

builder.Services
	.AddControllers()
	.AddNewtonsoftJson();

var app = builder.Build();

app.MapBotWebhookRoute<BotController>(route: botConfiguration.Route);
app.MapControllers();
app.Run();




public class BotConfiguration
{
	public static readonly string Configuration = "BotConfiguration";

	public string BotToken { get; init; } = default!;
	public string HostAddress { get; init; } = default!;
	public string Route { get; init; } = default!;
	public string SecretToken { get; init; } = default!;
}