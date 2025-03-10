using System.Diagnostics;

namespace SampleMAUI;

public partial class SecondaryPage : ContentPage
{
	public SecondaryPage()
	{
		InitializeComponent();
	}

	public async void SomeSuperAction(object sender, EventArgs e)
	{
		Debug.WriteLine("Marty !");
		await Shell.Current.GoToAsync($"///MainPage");
	}
}