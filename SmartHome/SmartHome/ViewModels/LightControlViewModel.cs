using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Caliburn.Micro;
using SmartHome.Infra;

namespace SmartHome.ViewModels
{
    public class LightControlViewModel:Screen
    {

        private DelegateCommand backNavigationCommand;
        private INavigationService navigationService;

        public LightControlViewModel(INavigationService navigationService)
        {
            backNavigationCommand=new DelegateCommand(GoBackNavigation);
            this.navigationService = navigationService;
        }

        public ICommand BackNavigationCommand => backNavigationCommand;

        private void GoBackNavigation(object sender)
        {
            navigationService.NavigateToViewModel<HomeViewModel>();
        }

    }
}
