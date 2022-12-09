using SharpHook.Native;
using TestAcuite.ViewModels;

namespace TestAcuite;

public partial class Acuite : ContentPage
{
    private AcuiteViewModel _viewModel;

    public Acuite()
	{
		InitializeComponent();
        BindingContext = new AcuiteViewModel();
        _viewModel = this.BindingContext as AcuiteViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.Listen();
    }
}

