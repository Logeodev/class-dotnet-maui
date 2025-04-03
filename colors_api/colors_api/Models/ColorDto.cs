using System.Text.Json.Serialization;

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
        [JsonPropertyName("type")]
        public ColorType Type { get; set; }
        [JsonPropertyName("red")]
        public int Red { get; set; }
        [JsonPropertyName("green")]
        public int Green { get; set; }
        [JsonPropertyName("blue")]
        public int Blue { get; set; }

        public ColorDto(ColorType name, int red, int green, int blue)
        {
            Type = name;
            Red = red;
            Green = green;
            Blue = blue;
        }
        public ColorDto()
        {
            
        }
    }
}
