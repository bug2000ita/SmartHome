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


        private double redValue = 0;
        public double RedValue
        {
            set
            {
                redValue = value;
                ApplyRed(value);
                
            }
        }


        private void ApplyRed(double value)
        {
            
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

        public double ColorCoordinateX
        {
            get
            {
                return Parameter?.ColorCoordinates[0] ?? 0;
            }
            set
            {
                Parameter?.ChangeColorCoordinates(value, ColorCoordinateY);
                
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
