using TestAcuite.ViewModels;
using SharpHook.Native;
namespace TestAcuite;

public partial class Calibration : ContentPage
{
	public Calibration()
	{
		InitializeComponent();
        BindingContext = new CalibrationViewModel();

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        UioHook.Stop();
    }

}

