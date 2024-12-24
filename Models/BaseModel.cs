
namespace GameZone.Models
{
    public class BaseModel
    {
        public int Id { get; set; }

        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

    }
}
