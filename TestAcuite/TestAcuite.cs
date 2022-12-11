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
            fonts.AddFont("Sloan.otf", "Sloan");
            fonts.AddFont("Antjac_Raskin_Landolt.otf", "RaskinLandol");
        }).UseMauiCommunityToolkit();

        builder.Services.AddSingleton<Acuite>();
        builder.Services.AddSingleton<AcuiteViewModel>();
        builder.Services.AddSingleton<Calibration>();
        builder.Services.AddSingleton<CalibrationViewModel>();
        return builder.Build();
    }
}