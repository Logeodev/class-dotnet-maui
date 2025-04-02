namespace colors_front.Models
{
    public class Color
    {
        public ColorType Type { get; set; }
        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }

        public Microsoft.Maui.Graphics.Color MauiColor =>
            new Microsoft.Maui.Graphics.Color(Red / 255f, Green / 255f, Blue / 255f);

        public string HexValue => $"#{Red:X2}{Green:X2}{Blue:X2}";

        public string TypeName => Type.ToString();
    }
}
