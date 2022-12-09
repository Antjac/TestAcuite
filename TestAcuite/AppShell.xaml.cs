namespace TestAcuite;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(Calibration), typeof(Calibration));
        Routing.RegisterRoute(nameof(Acuite), typeof(Acuite));
    }

}
