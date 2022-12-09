using TestAcuite.ViewModels;

namespace TestAcuite;

public partial class Calibration : ContentPage
{
	public Calibration()
	{
		InitializeComponent();
        BindingContext = new CalibrationViewModel();

    }
}

