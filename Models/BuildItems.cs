namespace WildRift.Telegram.Bot.Models
{
    public class BuildItems : BaseModel
    {
        public long ItemId { get; set; }
        public string StickerId { get; set; }
        public string Name { get; set; }
        public string Stats { get; set; }
        public string Passive { get; set; }
        public string UpdatedPatch { get; set; }
    }
}
