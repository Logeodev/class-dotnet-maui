namespace colors_api.Models
{
    public class Color
    {
        public int Id { get; set; }
        public ColorType Type { get; set; }
        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public override bool Equals(object? obj)
        {
            if(obj is Color other)
            {
                return Red == other.Red &&
                       Green == other.Green &&
                       Blue == other.Blue;
            }
            return false;
        }
    }
}
