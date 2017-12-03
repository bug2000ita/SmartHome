using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using HueLibrary;
using SmartHome.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SmartHome.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomeView 
    {

        //Bridge bridge = new Bridge("192.168.2.2", "3TkCwQ2eiHdB4cp9cVgaEayWibS-AqrBZxLBhmD0");

        public HomeView()
        {
            InitializeComponent();
        }

        //private async void Slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        //{
        //    var lights = await bridge.GetLightsAsync();

        //    foreach (var light in lights)
        //    {
        //        if (light.Name == "Sala")
        //        {

        //            light.State.On = true;
        //            light.State.Saturation = (byte)e.NewValue;
        //            light.State.Brightness = (byte)e.NewValue;
        //            light.State.Hue = (ushort)((byte)(e.NewValue) * 100);

        //        }
        //    }
        //}
    }
}
