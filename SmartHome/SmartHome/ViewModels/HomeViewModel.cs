using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Windows.UI.Xaml.Controls.Primitives;
using Caliburn.Micro;
using HueLibrary;
using SmartHome.Infra;
using SmartHome.Model;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace SmartHome.ViewModels
{
    public class HomeViewModel:Screen
    {
        private double lightValue = 50;
        private Bridge bridge;
        private IProxy HueProxy = new HueProxy();
        private readonly Brush OnLightBackground = new SolidColorBrush(Colors.Yellow);
        private readonly Brush OffLightBackground = new SolidColorBrush(Colors.Black);


        private Brush isLightOneOn;
        private Brush isLightTwoOn;
        private Brush isLightThreeOn;
        private Brush isLightSalaOn;
        private Brush isLightFlavioOn;
        private Brush isLightPranzoOn;
        private Brush isLightLettoOn;

        public RangeBaseValueChangedEventHandler HandlerSliderValueChanged;

        public ICommand ButtonCommand { get; set; }
        public ICommand ButtonLightCommand { get; set; }


        public Brush LightOneBackground;
        public Brush LightTwoBackground;
        public Brush LightThreeBackground;
        public Brush LightSalaBackground;

        
        public Brush IsLightLettoOn
        {
            get { return isLightLettoOn; }
            private set
            {
                isLightLettoOn = value;
                RisePropertyChange(nameof(IsLightLettoOn));
            }
        }
        public Brush IsLightFlavioOn
        {
            get { return isLightFlavioOn; }
            private set
            {
                isLightFlavioOn = value;
                RisePropertyChange(nameof(IsLightFlavioOn));
            }
        }
        public Brush IsLightPranzoOn
        {
            get { return isLightPranzoOn; }
            private set
            {
                isLightPranzoOn = value;
                RisePropertyChange(nameof(IsLightPranzoOn));
            }
        }
        public Brush IsLightOneOn
        {
            get { return isLightOneOn; }
            private set
            {
                isLightOneOn = value;
                RisePropertyChange(nameof(IsLightOneOn));
            }
        }

        public Brush IsLightTwoOn
        {
            get { return isLightTwoOn; }
            private set
            {
                isLightTwoOn = value;
                RisePropertyChange(nameof(IsLightTwoOn));
            }
        }
        public Brush IsLightThreeOn
        {
            get { return isLightThreeOn; }
            private set
            {
                isLightThreeOn = value;
                RisePropertyChange(nameof(IsLightThreeOn));
            }
        }

        public Brush IsLightSalaOn
        {
            get { return isLightSalaOn; }
            private set
            {
                isLightSalaOn = value;
                RisePropertyChange(nameof(IsLightSalaOn));
            }
        }


        public bool IsDefaultOn =>false;
       


        public string LightName { get; set; }
        public string Value { get; set; }


        private void Initialize(List<string> names)
        {
            if (names.Contains("Corridoio 1"))
            {
                IsLightOneOn = HueProxy.GetLightByName("Corridoio 1").IsOn() ? OnLightBackground : OffLightBackground;
            }
            if (names.Contains("Corridoio 2"))
            {
                IsLightTwoOn = HueProxy.GetLightByName("Corridoio 2").IsOn() ? OnLightBackground : OffLightBackground;
            }
            if (names.Contains("Corridoio 3"))
            {
                IsLightThreeOn = HueProxy.GetLightByName("Corridoio 3").IsOn() ? OnLightBackground : OffLightBackground;
            }

            if (names.Contains("Sala"))
            {
                IsLightSalaOn = HueProxy.GetLightByName("Sala").IsOn() ? OnLightBackground : OffLightBackground;
            }

            if (names.Contains("Flavio"))
            {
                IsLightFlavioOn = HueProxy.GetLightByName("Flavio").IsOn() ? OnLightBackground : OffLightBackground;
            }
            if (names.Contains("Luce pranzo"))
            {
                IsLightPranzoOn = HueProxy.GetLightByName("Luce pranzo").IsOn() ? OnLightBackground : OffLightBackground;
            }
            if (names.Contains("Letto"))
            {
                IsLightLettoOn = HueProxy.GetLightByName("Letto").IsOn() ? OnLightBackground : OffLightBackground;
            }

        }

        public HomeViewModel()
        {


            ButtonCommand =new DelegateCommand(PressButton);
            ButtonLightCommand = new DelegateCommand(PressButtonLight);

            HueProxy.Connect("192.168.2.2");
            var names = HueProxy.GetDeviceNamesAsync();/*.ContinueWith(
                t => Initialize(t.Result));*/
        }


        private void PressButton(object sender)
        {
            //var names = HueProxy.DeviceNames;
            //string buttonName = names.Cast<object>().Aggregate("", (current, name) => current + (" " + name));
            //Value = buttonName;
            //RisePropertyChange(nameof(Value));

            Initialize(HueProxy.DeviceNames as List<string>);
        }

        private void PressButtonLight(object sender)
        {
            //var names = HueProxy.DeviceNames;
            //string buttonName = names.Cast<object>().Aggregate("", (current, name) => current + (" " + name));
            //Value = buttonName;
            //RisePropertyChange(nameof(Value));

            ILight light = HueProxy.GetLightByName((sender as Button).Name);
            if (light.IsOn())
            {
                light.SwitchOff();
            }
            else
            {
                light.SwitchOn();
            }
        }

        public double LightValue 
        {
            get
            {
                return lightValue;
            }

            set
            {
                ChangeHueValue("Sala", value);
                lightValue = value;
            }
        }
        

        private async void ChangeHueValue(string name, double hueValue)
        {
            var lights = await bridge.GetLightsAsync();

            foreach (var light in lights)
            {
                if (light.Name == name)
                {

                    light.State.On = true;
                    light.State.Saturation = (byte) hueValue;
                }
            }
        }

        private void RisePropertyChange(string propertyName)
        {

            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));

        }



    }


}