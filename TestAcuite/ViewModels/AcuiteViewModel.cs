using System.ComponentModel;
using System.Runtime.CompilerServices;
using TestAcuite.Class;
using TestAcuite.Helpers;
using SharpHook;
namespace TestAcuite.ViewModels
{


    class AcuiteViewModel : INotifyPropertyChanged, IDisposable
    {
        private double _fontSize;
        private CalibrationParams _params;
        private String _textToShow;
        private Color _textColor;
        private Color _backgroundColor;
        private List<String> _lstText = new List<String>() { "NCKZO", "RHSDK", "DOVHR", "ONHRC", "DKSNV", "ZSOKN", "CKDNR", "SRZKD", "HZOVC", "NVDOK", "VHCNO", "SVHCZ", "OZDVK" };
        public SharpHook.SimpleGlobalHook hook;
        public AcuiteViewModel()
        {
            _params = ConfigHelper.GetCalibration();
            FontSize = _params.FontSize;
            _textColor = Color.Parse("White");
            _backgroundColor = Color.Parse("Black");
            _textToShow = "ABC";
            Listen();
        }

        private void Listen()
        {
            if (hook != null) { return; }
            hook = new SimpleGlobalHook();
            hook.KeyPressed += OnKeyPressed;
            hook.RunAsync();
        }

        private void OnKeyPressed(object sender, KeyboardHookEventArgs e)
        {
            var random = new Random();
            int index = random.Next(_lstText.Count);
            switch (e.Data.KeyCode)
            {
       
                case SharpHook.Native.KeyCode.VcNumPad3:
                    TextToShow = _lstText[index].Substring(0,3);
                    break;
                case SharpHook.Native.KeyCode.VcNumPad4:
                    TextToShow = _lstText[index].Substring(0, 4);
                    break;
                case SharpHook.Native.KeyCode.VcNumPad5:
                    TextToShow = _lstText[index].Substring(0, 5);
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

                case SharpHook.Native.KeyCode.VcNumPadAdd:
                    IncreaseTextSize();
                    break;
                case SharpHook.Native.KeyCode.VcNumPadSubtract:
                    DecreaseTextSize();
                    break;

                case SharpHook.Native.KeyCode.VcEscape:
                    Shell.Current.GoToAsync("..");
                    hook.Dispose();
                    break;
            }

        }

        private void IncreaseTextSize()
        {
            FontSize *= 1.2589d;
        }

        private void DecreaseTextSize()
        {
            FontSize /= 1.2589d;
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
        public Color BackGroundColor
        {
            get { return _backgroundColor; }
            set { _backgroundColor = value; OnPropertyChanged("BackGroundColor"); }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public void Dispose()
        {
            if (!hook.IsDisposed)
            {
                hook.Dispose();
            }

        }
    }
}
