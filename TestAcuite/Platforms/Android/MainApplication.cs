using Android.App;
using Android.Runtime;
using TestAcuite.Class;

namespace TestAcuite;

[Application]
public class MainApplication : MauiApplication
{
	public MainApplication(IntPtr handle, JniHandleOwnership ownership)
		: base(handle, ownership)
	{
	}

	protected override MauiApp CreateMauiApp() => Calibration.CreateMauiApp();
}
