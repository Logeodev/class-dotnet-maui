using System.Collections;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ColorManager.ViewModels
{
    public partial class ColorsViewModel : ObservableObject
    {
        public ObservableCollection<ColorItemViewModel> Colors { get; } = new ObservableCollection<ColorItemViewModel>();

        public ColorsViewModel()
        {
            LoadColors();
        }

        private void LoadColors()
        {
            List<ColorItemViewModel> colorsList = new List<ColorItemViewModel>
            {
                new ColorItemViewModel("Red", Microsoft.Maui.Graphics.Colors.Red),
                new ColorItemViewModel("Blue", Microsoft.Maui.Graphics.Colors.Blue),
                new ColorItemViewModel("Green", Microsoft.Maui.Graphics.Colors.Green),
                new ColorItemViewModel("Gold", Microsoft.Maui.Graphics.Colors.Goldenrod),
                new ColorItemViewModel("Pink", Microsoft.Maui.Graphics.Colors.HotPink)
            };

            foreach (var color in colorsList)
            {
                Colors.Add(color);
            }
        }

        public void AddColor(ColorItemViewModel color)
        {
            Colors.Add(color);
        }

        public void RemoveColor(ColorItemViewModel color)
        {
            Colors.Remove(color);
        }
    }
}
