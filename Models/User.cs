namespace WildRift.Telegram.Bot.Models
{
	public class User
	{
		public long Id { get; set; }
		public long ChatId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string UserName { get; set; }
		public bool IsBot { get; set; }
        public string LanguageCode { get; set; }
        public DateTime StartedDate { get; set; } = DateTime.UtcNow;
	}
}
