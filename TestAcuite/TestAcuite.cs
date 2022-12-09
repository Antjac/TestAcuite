using TestAcuite.ViewModels;
using CommunityToolkit.Maui;

namespace TestAcuite;
public static class TestAcuite
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>().ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            fonts.AddFont("Optician-Sans.otf", "OpticianSans");
            fonts.AddFont("Sloan.otf", "Sloan");
        }).UseMauiCommunityToolkit();

        builder.Services.AddSingleton<Acuite>();
        builder.Services.AddSingleton<AcuiteViewModel>();
        builder.Services.AddSingleton<Calibration>();
        builder.Services.AddSingleton<CalibrationViewModel>();
        return builder.Build();
    }
}