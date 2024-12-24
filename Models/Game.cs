
namespace GameZone.Models
{
    public class Game:BaseModel
    {
        
        [MaxLength(2500)]
        public string Description { get; set; } = string.Empty;

        [MaxLength(500)]
        public string CoverUrl { get; set; }=string.Empty;

        public int CategoryID { get; set; }

        public Category Category { get; set; } = default!;
        public ICollection<GameDevices> Devices { get; set; } = new List<GameDevices>();
    }
}
