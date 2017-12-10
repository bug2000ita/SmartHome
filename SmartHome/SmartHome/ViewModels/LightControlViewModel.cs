using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls.Primitives;
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
        private float intensity;

        public LightControlViewModel(INavigationService navigationService)
        {


            backNavigationCommand =new DelegateCommand(GoBackNavigation);
            switchLightCommand = new DelegateCommand(SwitchLight);

            this.navigationService = navigationService;


        }
        


        public ImageSource Image => IsOn?  
            new BitmapImage(new Uri("ms-appx:///LightOn.png")) : 
            new BitmapImage(new Uri("ms-appx:///LightOff.png"));

        public ICommand BackNavigationCommand => backNavigationCommand;
        public ICommand SwitchLightCommand => switchLightCommand;
        private void SwitchLight(object sender)
        {

            if (parameter.IsOn())
            {
                parameter.SwitchOff();
            }
            else
            {
                parameter.SwitchOn();
            }
        }

        public bool IsOn => parameter.IsOn();



        public ILight Parameter
        {
            get { return parameter; }
            set
            {
                if (value.Equals(parameter)) return;
                parameter = value;
                NotifyOfPropertyChange(() => Parameter);
            }
        }


        public string Name => parameter.Name;


        public float Intensity
        {
            get { return intensity; }
            set
            {
                intensity = value;
                Parameter.ChangeBrightness((byte)value);
            }
        }


        private void GoBackNavigation(object sender)
        {
            navigationService.NavigateToViewModel<HomeViewModel>();
        }



    }
}
