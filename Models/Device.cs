
namespace GameZone.Models
{
    public class Device:BaseModel
    {


        [MaxLength(500)]
        public string IconUrL { get; set; } = string.Empty;

        public ICollection<GameDevices> Games { get; set; } = new List<GameDevices>();


    }
}
