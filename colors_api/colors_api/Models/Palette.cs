namespace colors_api.Models
{
    public class Palette
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Color> Colors { get; set; } = new List<Color>();
    }
}
