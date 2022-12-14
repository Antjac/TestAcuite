using System.ComponentModel;
using System.Runtime.CompilerServices;
using TestAcuite.Class;
using TestAcuite.Helpers;
using SharpHook;

namespace TestAcuite.ViewModels
{
    class AcuiteViewModel : INotifyPropertyChanged
    {
        private double _fontSize;
        private CalibrationParams _params;
        private String _textToShow;
        private Color _textColor;
        private Color _backgroundColor;
        private decimal _currentLogMar;
        private List<String> _lstToShow = new();
        private readonly List<String> _lstSloan = new() { "NCKZO", "RHSDK", "DOVHR", "ONHRC", "DKSNV", "ZSOKN", "CKDNR", "SRZKD", "HZOVC", "NVDOK", "VHCNO", "SVHCZ", "OZDVK" };
        private readonly List<String> _lstRaskin = new() { "ADBCD", "CBADC", "DCBAB", "BADCA" };
        private readonly List<String> _lstLandolt = new() { "EKGHI", "LFEKJ", "KEHFI", "FKHIG", "EJFLH","HKIHL","IJFEG","FIHEK" };
        private const double LOGMAR_COEFF = 1.2589d;
        private int _nbLetters;
        private int _scaleX;
        private string _fontToUse;
        private readonly ConvertUnit[] _convert = new ConvertUnit[]
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
        public SimpleGlobalHook hook = new();

        #region Constructors
        public AcuiteViewModel()
        {
            _isHelpVisible = true;
            _isAcuiteVisible = false;
            _textColor = Color.Parse("White");
            _backgroundColor = Color.Parse("Black");
            _isAcuiteVisible = true;
            _scaleX = 1;
            _nbLetters = 3;
            _fontToUse = "Sloan";
            _lstToShow = _lstSloan;
            hook.KeyPressed += OnKeyPressed;
            Listen();
            ShowCombinaison();
        }
        #endregion

        #region Properties
        public String TextToShow
        {
            get { return _textToShow; }
            set { _textToShow = value; OnPropertyChanged(nameof(TextToShow)); }
        }

        public String FontToUse
        {
            get { return _fontToUse; }
            set { _fontToUse = value; OnPropertyChanged(nameof(FontToUse)); }
        }

        public double FontSize
        {
            get { return _fontSize; }
            set { _fontSize = value; OnPropertyChanged(nameof(FontSize)); }
        }

        public Color TextColor
        {
            get { return _textColor; }
            set { _textColor = value; OnPropertyChanged(nameof(TextColor)); }

        }

        public String AcuiteText
        {
            get 
            {
                ConvertUnit currentVal = _convert.FirstOrDefault(x => x.Logmar.Equals(_currentLogMar));
             
                return "LogMar : " + currentVal.Logmar.ToString() + Environment.NewLine + "Monoyer : " + currentVal.Monoyer + Environment.NewLine + "Décimal : " + currentVal.DecFrac.ToString(); 
            }
        }

        public Boolean IsAcuiteVisible
        {
            get { return _isAcuiteVisible; }
            set { _isAcuiteVisible = value; OnPropertyChanged(nameof(IsAcuiteVisible)); }
        }

        public int ScaleXValue
        {
            get { return _scaleX; }
            set { _scaleX = value; OnPropertyChanged(nameof(ScaleXValue)); }
        }

        public Boolean IsHelpVisible
        {
            get { return _isHelpVisible; }
            set { _isHelpVisible = value; OnPropertyChanged(nameof(IsHelpVisible)); }
        }


        public Color BackGroundColor
        {
            get { return _backgroundColor; }
            set { _backgroundColor = value; OnPropertyChanged(nameof(BackGroundColor)); }
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion Events

        #region Voids
        public void Listen()
        {
            _params = ConfigHelper.GetCalibration();
            _currentLogMar = _params.Accuity;
            FontSize = _params.FontSize;
            if (!hook.IsRunning)
            {
                hook.RunAsync();
            }
            OnPropertyChanged(nameof(AcuiteText));
        }

        private void OnKeyPressed(object sender, KeyboardHookEventArgs e)
        {
            switch (e.Data.KeyCode)
            {
                case SharpHook.Native.KeyCode.VcNumPad1:
                    _nbLetters = 1;
                    ShowCombinaison();
                    break;

                case SharpHook.Native.KeyCode.VcNumPad2:
                    _nbLetters = 2;
                    ShowCombinaison();
                    break;
                case SharpHook.Native.KeyCode.VcNumPad3:
                    _nbLetters = 3;
                    ShowCombinaison();
                    break;
                case SharpHook.Native.KeyCode.VcNumPad4:
                    _nbLetters = 4;
                    ShowCombinaison();
                    break;
                case SharpHook.Native.KeyCode.VcNumPad5:
                    _nbLetters = 5;
                    ShowCombinaison();
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
                case SharpHook.Native.KeyCode.VcM:
                    ScaleXValue = ScaleXValue *= -1;
                    break;
                case SharpHook.Native.KeyCode.VcF1:
                    FontToUse = "Sloan";
                    _lstToShow = _lstSloan;
                    ShowCombinaison();
                    break;
                case SharpHook.Native.KeyCode.VcF2:
                    FontToUse = "RaskinLandol";
                    _lstToShow = _lstRaskin;
                    ShowCombinaison();
                    break;
                case SharpHook.Native.KeyCode.VcF3:
                    FontToUse = "RaskinLandol";
                    _lstToShow = _lstLandolt;
                    ShowCombinaison();
                    break;
                case SharpHook.Native.KeyCode.VcNumPadAdd:
                    DecreaseTextSize();
                    break;
                case SharpHook.Native.KeyCode.VcNumPadSubtract:
                    IncreaseTextSize();
                    break;

                case SharpHook.Native.KeyCode.VcEscape:
                    Shell.Current.GoToAsync("..");
                    break;
            }
        }

        private void ShowCombinaison()
        {
            var random = new Random();
            int index = random.Next(_lstToShow.Count);
            TextToShow = _lstToShow[index].Substring(random.Next(0, _lstToShow[index].Count() - _nbLetters), _nbLetters);
        }
        private void IncreaseTextSize()
        {
            ConvertUnit nextVal = _convert.FirstOrDefault(x => x.Logmar.Equals(_currentLogMar + 0.1M));
            if (nextVal == null)
            {
                return;
            }

            FontSize *= LOGMAR_COEFF;
            _currentLogMar = nextVal.Logmar;
            OnPropertyChanged(nameof(AcuiteText));
            ShowCombinaison();
        }

        private void DecreaseTextSize()
        {
            ConvertUnit nextVal = _convert.FirstOrDefault(x => x.Logmar.Equals(_currentLogMar - 0.1M));
            if (nextVal == null)
            {
                return;
            }

            FontSize /= LOGMAR_COEFF;
            _currentLogMar = nextVal.Logmar;
            OnPropertyChanged(nameof(AcuiteText));
            ShowCombinaison();
        }


        public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        #endregion
    }
}
