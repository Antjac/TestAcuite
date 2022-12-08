using TestAcuite.ViewModels;

namespace TestAcuite;

public partial class Acuite : ContentPage
{
	public Acuite()
	{
		InitializeComponent();
        BindingContext = new AcuiteViewModel();
        AcuiteViewModel viewModel = this.BindingContext as AcuiteViewModel;
    }
}

