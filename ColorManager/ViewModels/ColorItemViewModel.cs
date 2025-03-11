using CommunityToolkit.Mvvm.ComponentModel;

namespace ColorManager.ViewModels
{
    public partial class ColorItemViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(RgbDetails))]
        private Color _color = Microsoft.Maui.Graphics.Colors.Blue;

        [ObservableProperty]
        private string _rgbDetails;

        public ColorItemViewModel(string name, Color color)
        {
            _name = name;
            _color = color;
            UpdateRgbDetails();
        }

        private void UpdateRgbDetails()
        {
            _color.ToRgb(out byte r, out byte g, out byte b);
            RgbDetails = $"RGB: {r}, {g}, {b}";
        }
    }
}
