using System.ComponentModel.DataAnnotations.Schema;

namespace GameZone.Models
{
    public class GameDevices
    {
        
        public int GameID { get; set; }
        
        public Game Game { get; set; } = default!;

        public int DeviceID { get; set; }
        
        public Device Device { get; set; } = default!;
    }
}
