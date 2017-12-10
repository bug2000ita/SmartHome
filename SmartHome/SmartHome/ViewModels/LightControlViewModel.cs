using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls.Primitives;
using Caliburn.Micro;
using SmartHome.Infra;
using SmartHome.Model;

namespace SmartHome.ViewModels
{
    public class LightControlViewModel:Screen
    {

        private DelegateCommand backNavigationCommand;
        private DelegateCommand switchLightCommand;
        private INavigationService navigationService;

        public LightControlViewModel(INavigationService navigationService)
        {
            backNavigationCommand=new DelegateCommand(GoBackNavigation);
            switchLightCommand = new DelegateCommand(SwitchLight);

            this.navigationService = navigationService;
        }

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

        private ILight parameter;

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


        private float intensity;
        public float Intensity
        {
            get { return intensity; }
            set
            {
                intensity = value;
                Parameter.ChangeBrightness((byte)value);
            }
        }


        public ICommand BackNavigationCommand => backNavigationCommand;
        public ICommand SwitchLightCommand => switchLightCommand;

        private void GoBackNavigation(object sender)
        {
            navigationService.NavigateToViewModel<HomeViewModel>();
        }



    }
}
