using System;
using System.ComponentModel;
using System.Windows.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Caliburn.Micro;
using SmartHome.Infra;
using SmartHome.Model;

namespace SmartHome.ViewModels
{
    public class LightControlViewModel:Screen
    {

        private DelegateCommand backNavigationCommand;
        private DelegateCommand switchLightCommand;
        private ILight parameter;
        private INavigationService navigationService;
        private float brightness;

        public LightControlViewModel(INavigationService navigationService)
        {


            backNavigationCommand =new DelegateCommand(GoBackNavigation);
            switchLightCommand = new DelegateCommand(SwitchLight);

            this.navigationService = navigationService;
            
        }





        private ImageSource image;
        public ImageSource Image
        {
            get
            {
                ImageSource localImage = null;
                var value = IsOn;

                if (value == true)
                {
                    localImage = new BitmapImage(new Uri("ms-appx:///LightOn.png"));
                }
                else if (value == false)
                {
                    localImage = new BitmapImage(new Uri("ms-appx:///LightOff.png"));
                }

                return localImage;
            }

            private set
            {
                image = value;
                RisePropertyChange(nameof(Image));
            }
        }


        public ICommand BackNavigationCommand => backNavigationCommand;
        public ICommand SwitchLightCommand => switchLightCommand;
        private void SwitchLight(object sender)
        {
            if (parameter == null) return;

            if (parameter.IsOn())
            {
                parameter.SwitchOff();
                Image = new BitmapImage(new Uri("ms-appx:///LightOff.png"));

            }
            else
            {
                parameter.SwitchOn();
                Image = new BitmapImage(new Uri("ms-appx:///LightOn.png"));
            }
        
        }

        public bool? IsOn => parameter?.IsOn();


        public ILight Parameter
        {
            get { return parameter; }
            set
            {
                if (value==null || value.Equals(parameter)) return;
                parameter = value;
                NotifyOfPropertyChange(() => Parameter);
            }
        }


        public string Name => parameter == null ? "Error Light Name" : parameter.Name;


        private byte redValue = 0;
        private byte greenValue = 0;
        private byte blueValue = 0;

        public string RedValue
        {
            set
            {
                byte convertedValue = byte.Parse(value);
                redValue = convertedValue;
                ApplyRGB();
                
            }
        }

        public string GreenValue
        {
            set
            {
                byte convertedValue = byte.Parse(value);
                greenValue = convertedValue;
                ApplyRGB();

            }
        }

        public string BlueValue
        {
            set
            {
                byte convertedValue = byte.Parse(value);
                blueValue = convertedValue;
                ApplyRGB();

            }
        }


        private void ApplyRGB()
        {
            RgbToXy(redValue, greenValue, blueValue);
        }


        public float Brightness
        {
            get
            {
                return Parameter?.Brightness ?? 0;
            }
            set
            {
                Parameter?.ChangeBrightness((byte)value);           
            }
        }

        public void  RgbToXy(byte r, byte g, byte b)
        {
            if(parameter==null) return;

            var red = PivotRgb(r / 255.0);
            var green = PivotRgb(g / 255.0);
            var blue = PivotRgb(b/ 255.0);

            var x = red * 0.4124 + green * 0.3576 + blue * 0.1805;
            var y = red * 0.2126 + green * 0.7152 + blue * 0.0722;
            var bri = red * 0.0193 + green * 0.1192 + blue * 0.9505;

            parameter.ChangeBrightness((byte)bri);
            parameter.ColorCoordinates[0] = x;
            parameter.ColorCoordinates[1] = y;
        }

        private  double PivotRgb(double n)
        {
            return (n > 0.04045 ? Math.Pow((n + 0.055) / 1.055, 2.4) : n / 12.92) ;
        }

        private void XyBrightToRgb()
        {
            double x = parameter.ColorCoordinates[0];
            double y = parameter.ColorCoordinates[1];
            byte bri = parameter.Brightness;

            var z = 1.0 - x - y;

            var yLocal = bri / 255.0; // Brightness of lamp
            var xLocal = (yLocal / y) * x;
            var zLocal = (yLocal / y) * z;
            var r = xLocal * 1.612 - yLocal * 0.203 - zLocal * 0.302;
            var g = -xLocal * 0.509 + yLocal * 1.412 + zLocal * 0.066;
            var b = xLocal * 0.026 - yLocal * 0.072 + zLocal * 0.962;
            r = r <= 0.0031308 ? 12.92 * r : (1.0 + 0.055) * Math.Pow(r, (1.0 / 2.4)) - 0.055;
            g = g <= 0.0031308 ? 12.92 * g : (1.0 + 0.055) * Math.Pow(g, (1.0 / 2.4)) - 0.055;
            b = b <= 0.0031308 ? 12.92 * b : (1.0 + 0.055) * Math.Pow(b, (1.0 / 2.4)) - 0.055;
            var maxValue = 0;
            maxValue = Math.Max((int)r, (int)g);
            maxValue = Math.Max( maxValue, (int)b);
            r /= maxValue;
            g /= maxValue;
            b /= maxValue;

            redValue = (byte)(Math.Round(r) * 255);
            if (r < 0)
            {
                redValue = 255;    
            }
            greenValue = (byte)(Math.Round(g) * 255);
            if (g < 0)
            {
                greenValue = 255;            
            }

            blueValue = (byte)(Math.Round(b) * 255);
            if (b < 0)
            {
                blueValue = 255; 
            }

        }

        public double ColorCoordinateX
        {
            get
            {
                return Parameter?.ColorCoordinates[0] ?? 0;
            }
            set
            {
                Parameter?.ChangeColorCoordinates(value, ColorCoordinateY);
                XyBrightToRgb();
            }
        }

        public double ColorCoordinateY
        {
            get
            {
                return Parameter?.ColorCoordinates[1] ?? 0;
            }
            set
            {
                Parameter?.ChangeColorCoordinates(ColorCoordinateX,value);
                XyBrightToRgb();
            }
        }

        public float Saturation
        {
            get
            {
                return Parameter?.Saturation ?? 0;
                
            }

            set
            {
                Parameter?.ChangeSaturation((byte)value);
            }
        }


        private void GoBackNavigation(object sender)
        {
            navigationService.NavigateToViewModel<HomeViewModel>();
        }


        private void RisePropertyChange(string propertyName)
        {

            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));

        }
    }
}
