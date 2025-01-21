namespace GameZone.Models
{
    public class Device:BaseEntity
    {
        public string Icon { get; set; }
        public ICollection<GameDevice> GameDevices { get; set; }

    }
}
