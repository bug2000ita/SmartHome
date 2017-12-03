using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HueLibrary;

namespace SmartHome.Model
{
    public class HueLight: ILight
    {
        
        private Light light;

        public string Name => light.Name;

        public HueLight(Light light)
        {
            this.light = light;
        }

        public void SwitchOn()
        {
            light.State.On = true;
        }

        public void SwitchOff()
        {
            light.State.On = false;
        }

        public void ChangeHue(ushort value)
        {
            light.State.Hue = value;
        }

        public void ChangeSaturation(byte value)
        {
            light.State.Saturation = value;
        }

        public void ChangeBrightness(byte value)
        {
            light.State.Brightness = value;
        }

        public void ChangeColorCoordinates(double x, double y)
        {
            light.State.ColorCoordinates = new []{x,y};
        }

        public bool IsOn()
        {
            return light.State.On;
        }


    }
}
