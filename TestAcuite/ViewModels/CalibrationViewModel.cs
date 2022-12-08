using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using TestAcuite.Class;
using TestAcuite.Helpers;

namespace TestAcuite.ViewModels
{
    internal class CalibrationViewModel : INotifyPropertyChanged
    {
        private CalibrationParams _params = new CalibrationParams();


        public ICommand CalibrationValidationCommand { get; private set; }
        public CalibrationViewModel()
        {
            CalibrationParams p = ConfigHelper.GetCalibration();
            if (p is null)
            {
                _params.Accuity = 0.5d;
                _params.TextSize = 10;
                _params.Distance = 300;
                _params.FontSize = 100;
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
                    ConfigHelper.SaveCalibration(_params);
                },
                canExecute: () =>
                {
                    return true;
                });
        }

        public int FontSize
        {
            get { return _params.FontSize; }
            set
            {
                _params.FontSize = value;
                OnPropertyChanged("FontSize");
            }
        }

        public double Accuity
        {
            get { return _params.Accuity; }
            set
            {
                _params.Accuity = value;
                OnPropertyChanged("Accuity");
            }
        }

        public int TextSize
        {
            get { return _params.TextSize; }
            set
            {
                _params.TextSize = value;
                OnPropertyChanged("TextSize");
            }
        }

        public int Distance
        {
            get { return _params.Distance; }
            set
            {
                _params.Distance = value;
                OnPropertyChanged("Distance");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
