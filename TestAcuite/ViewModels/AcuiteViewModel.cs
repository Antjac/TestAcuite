using System.ComponentModel;
using System.Runtime.CompilerServices;
using TestAcuite.Class;
using TestAcuite.Helpers;
using SharpHook;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using SharpHook.Native;

namespace TestAcuite.ViewModels
{
    class AcuiteViewModel : INotifyPropertyChanged, IDisposable
    {
        private double _fontSize;
        private CalibrationParams _params;
        private String _textToShow;
        private Color _textColor;
        private Color _backgroundColor;
        private decimal _currentLogMar;
        private List<String> _lstText = new List<String>() { "NCKZO", "RHSDK", "DOVHR", "ONHRC", "DKSNV", "ZSOKN", "CKDNR", "SRZKD", "HZOVC", "NVDOK", "VHCNO", "SVHCZ", "OZDVK" };
        public SharpHook.SimpleGlobalHook hook = new SimpleGlobalHook();

        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        ConvertUnit[] _convert = new ConvertUnit[]
        {
            new ConvertUnit(-0.3M,2,"20/10"),
            new ConvertUnit(-0.2M,1.6,"16/10"),
            new ConvertUnit(-0.1M,1.25,"12,5/10"),
            new ConvertUnit(0,1,"10/10"),
            new ConvertUnit(0.1M,0.8,"8/10"),
            new ConvertUnit(0.2M,0.63,"6,3/10"),
            new ConvertUnit(0.3M,0.5,"5/10"),
            new ConvertUnit(0.4M,0.4,"4/10"),
            new ConvertUnit(0.5M,0.32,"3.2/10"),
            new ConvertUnit(0.6M,0.25,"2.5/10"),
            new ConvertUnit(0.7M,0.2,"2/10"),
            new ConvertUnit(0.8M,0.16,"1.6/10"),
            new ConvertUnit(0.9M,0.125,"1.25/10"),
            new ConvertUnit(1M,0.1,"1/10"),
            new ConvertUnit(1.1M,0.08,"1/12"),
            new ConvertUnit(1.2M,0.063,"1/16"),
            new ConvertUnit(1.3M,0.05,"1/20"),
            new ConvertUnit(1.4M,0.04,"1/25"),
            new ConvertUnit(1.5M,0.033,"1/30"),
            new ConvertUnit(1.6M,0.025,"1/40"),
            new ConvertUnit(1.7M,0.020,"1/50")
            /*new ConvertUnit(1.8M,0.016,"1/60"),
            new ConvertUnit(1.9M,0.0125,"1/80"),
            new ConvertUnit(2M,0.010,"1/100"),
            new ConvertUnit(2.1M,0.008,"1/120")*/
        };
        private bool _isAcuiteVisible;
        private bool _isHelpVisible;

        public AcuiteViewModel()
        {
            _isHelpVisible = true;
            _isAcuiteVisible = false;
            _textColor = Color.Parse("White");
            _backgroundColor = Color.Parse("Black");
            _isAcuiteVisible = false;
            hook.KeyPressed += OnKeyPressed;
            Listen();
        }

        public void Listen()
        {
            _params = ConfigHelper.GetCalibration();
            _currentLogMar = _params.Accuity;
            FontSize = _params.FontSize;
            int index = new Random().Next(_lstText.Count);
            TextToShow = _lstText[index].Substring(0, 3);
            if (!hook.IsRunning)
            {
                hook.RunAsync();
            }
        }

        private void OnKeyPressed(object sender, KeyboardHookEventArgs e)
        {
            var random = new Random();
            int index = random.Next(_lstText.Count);
            switch (e.Data.KeyCode)
            {
                case SharpHook.Native.KeyCode.VcNumPad2:
                    TextToShow = _lstText[index].Substring(0, 2);
                    break;
                case SharpHook.Native.KeyCode.VcNumPad3:
                    TextToShow = _lstText[index].Substring(0,3);
                    break;
                case SharpHook.Native.KeyCode.VcNumPad4:
                    TextToShow = _lstText[index].Substring(0, 4);
                    break;
                case SharpHook.Native.KeyCode.VcNumPad5:
                    TextToShow = _lstText[index].Substring(0, 5);
                    break;
                case SharpHook.Native.KeyCode.VcEnter:
                    IsAcuiteVisible = !IsAcuiteVisible;
                    break;
                case SharpHook.Native.KeyCode.VcSpace:
                    if (TextColor == Color.Parse("Black"))
                    {
                        TextColor = Color.Parse("White");
                        BackGroundColor = Color.Parse("Black");
                    }
                    else
                    {
                        TextColor = Color.Parse("Black");
                        BackGroundColor = Color.Parse("White");
                    }
                break;
                case SharpHook.Native.KeyCode.VcH:
                    IsHelpVisible = !IsHelpVisible;
                    break;
                case SharpHook.Native.KeyCode.VcNumPadAdd:
                    IncreaseTextSize();
                    break;
                case SharpHook.Native.KeyCode.VcNumPadSubtract:
                    DecreaseTextSize();
                    break;

                case SharpHook.Native.KeyCode.VcEscape:
                    Shell.Current.GoToAsync("..");
                    break;
            }

        }

        private void IncreaseTextSize()
        {
            ConvertUnit nextVal = _convert.FirstOrDefault(x => x.Logmar.Equals(_currentLogMar + 0.1M));
            if (nextVal == null)
            {
                return;
            }
            
            FontSize *= 1.2589d;
            _currentLogMar = nextVal.Logmar;
            OnPropertyChanged(nameof(AcuiteText));
            showToast(AcuiteText); 
        }

        private void DecreaseTextSize()
        {
            ConvertUnit nextVal = _convert.FirstOrDefault(x => x.Logmar.Equals(_currentLogMar - 0.1M));
            if (nextVal == null)
            {
                return;
            }

            FontSize /= 1.2589d;
            _currentLogMar = nextVal.Logmar;
            OnPropertyChanged(nameof(AcuiteText));
            showToast(AcuiteText);

        }

        public String TextToShow
        {
            get { return _textToShow; }
            set { _textToShow = value; OnPropertyChanged("TextToShow"); }
        }
        public double FontSize
        {
            get { return _fontSize; }
            set { _fontSize = value; OnPropertyChanged("FontSize"); }
        }

        public Color TextColor
        {
            get { return _textColor; }
            set { _textColor = value; OnPropertyChanged("TextColor"); }

        }

        public String AcuiteText
        {
            get 
            {
                ConvertUnit nextVal = _convert.FirstOrDefault(x => x.Logmar.Equals(_currentLogMar + 0.1M));
                return "LogMar : " + nextVal.Logmar.ToString() + Environment.NewLine + "Monoyer : " + nextVal.Monoyer + Environment.NewLine + "Décimal : " + nextVal.DecFrac.ToString(); 
            }
        }

        public Boolean IsAcuiteVisible
        {
            get { return _isAcuiteVisible; }
            set { _isAcuiteVisible = value; OnPropertyChanged("IsAcuiteVisible"); }
        }

        public Boolean IsHelpVisible
        {
            get { return _isHelpVisible; }
            set { _isHelpVisible = value; OnPropertyChanged("IsHelpVisible"); }
        }


        public Color BackGroundColor
        {
            get { return _backgroundColor; }
            set { _backgroundColor = value; OnPropertyChanged("BackGroundColor"); }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        async private void showToast(string message)
        {
            ToastDuration duration = ToastDuration.Long;
            double fontSize = 50;
            var toast = Toast.Make(message, duration, fontSize);

            await toast.Show(cancellationTokenSource.Token);
        }

        public void Dispose()
        {
            if (!hook.IsDisposed)
            {
                hook.Dispose();
            }

        }
    }
}
