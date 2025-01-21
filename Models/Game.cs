namespace GameZone.Models
{
    public class Game:BaseEntity
    {
        public string Description { get; set; }
        public string Cover { get; set; }

        public int CategoryID { get; set; }
        public Category Category { get; set; }
        public ICollection<GameDevice> GameDevices { get; set; }

    }
}
