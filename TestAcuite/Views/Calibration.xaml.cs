using TestAcuite.ViewModels;

namespace TestAcuite;

public partial class Calibration : ContentPage
{
	public Calibration()
	{
		InitializeComponent();
        BindingContext = new CalibrationViewModel();

    }

    async void OnRunClicked(object sender, EventArgs args)
    {
        await Navigation.PushAsync(new Acuite());
    }

}

