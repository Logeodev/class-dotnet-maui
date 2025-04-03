namespace colors_api.Models
{
    public class Palette
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Color[] Colors { get; set; } = Array.Empty<Color>();
    }
}
