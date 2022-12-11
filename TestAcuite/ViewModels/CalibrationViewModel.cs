using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TestAcuite.Class;
using TestAcuite.Helpers;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
namespace TestAcuite.ViewModels
{
    internal class CalibrationViewModel : INotifyPropertyChanged
    {
        private CalibrationParams _params = new CalibrationParams();
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand CalibrationValidationCommand { get; private set; }
        public ICommand GoToAccuiteCommand { get; private set; }

        #region Construcors
        public CalibrationViewModel()
        {
            CalibrationParams p = ConfigHelper.GetCalibration();
            if (p is null)
            {
                _params.Accuity = 1.0M;
                _params.TextSize = 7;
                _params.Distance = 400;
                _params.FontSize = 500;
                ConfigHelper.SaveCalibration(_params);
            }
            else
            {
                _params.Accuity = p.Accuity;
                _params.TextSize = p.TextSize;
                _params.Distance = p.Distance;
                _params.FontSize = p.FontSize;
            }

            CalibrationValidationCommand = new Command(
                execute: () =>
                {
                    if (ConfigHelper.SaveCalibration(_params))
                    {
                        ShowSaveCalibrationToast();
                    }
                },
                canExecute: () =>
                {
                    return true;
                });

            GoToAccuiteCommand = new Command(
               execute: () =>
               {
                   Shell.Current.GoToAsync(nameof(Acuite));
               },
               canExecute: () =>
               {
                   return true;
               });

        }
        #endregion

        #region Properties
        public int FontSize
        {
            get { return _params.FontSize; }
            set
            {
                _params.FontSize = value;
                OnPropertyChanged(nameof(FontSize));
            }
        }

        public decimal Accuity
        {
            get { return _params.Accuity; }
            set
            {
                _params.Accuity = value;
                OnPropertyChanged(nameof(Accuity));
            }
        }

        public int TextSize
        {
            get { return _params.TextSize; }
            set
            {
                _params.TextSize = value;
                OnPropertyChanged(nameof(TextSize));
            }
        }

        public int Distance
        {
            get { return _params.Distance; }
            set
            {
                _params.Distance = value;
                OnPropertyChanged(nameof(Distance));
            }
        }
        #endregion

        #region voids
        private async void ShowSaveCalibrationToast()
        {
            ToastDuration duration = ToastDuration.Long;
            double fontSize = 50;
            var toast = Toast.Make("Calibration sauvegardée", duration, fontSize);

            await toast.Show(cancellationTokenSource.Token);
        }

        public void OnPropertyChanged([CallerMemberName] string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        #endregion
    }
}
