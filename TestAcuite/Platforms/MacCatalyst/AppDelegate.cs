using Foundation;
using TestAcuite.Class;

namespace TestAcuite;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
	protected override MauiApp CreateMauiApp() => Calibration.CreateMauiApp();
}
