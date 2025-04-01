namespace colors_api.Models
{
    public enum ColorType
    {
        Primary,
        Secondary,
        Tertiary,
        Complementary,
        Accent
    }
    public class ColorDto
    {
        public ColorType Type { get; set; }
        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }

        public ColorDto(ColorType name, int red, int green, int blue)
        {
            Type = name;
            Red = red;
            Green = green;
            Blue = blue;
        }
    }
}
