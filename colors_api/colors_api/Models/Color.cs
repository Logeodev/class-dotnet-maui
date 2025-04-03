namespace colors_api.Models
{
    public class Color
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
