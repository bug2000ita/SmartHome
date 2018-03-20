
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HueLibrary;

namespace SmartHome.Model
{
    public class HueProxy:IProxy
    {

        private Bridge hueBridge;
        private IEnumerable<Light> devices = new List<Light>();

        public IEnumerable DeviceNames
        {
            get
            {
                return devices.Select(device => device.Name).ToList();
            }
        }

        public ILight GetLightByName(string name)
        {
            var light = devices.First(deviceAvailable => deviceAvailable.Name == name);
            return light != null ? new HueLight(light) : null;
        }

        public ILight GetLightContainsName(string name)
        {
            try
            {
                name = name.Replace('_', ' ');
                var light = devices.First(deviceAvailable => deviceAvailable.Name.Contains(name));
                return light != null ? new HueLight(light) : null;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return null;
            }
        }

        public async void Connect(string ip)
        {
            //192.168.2.5
            try
            {
                hueBridge = new Bridge(ip, "3TkCwQ2eiHdB4cp9cVgaEayWibS-AqrBZxLBhmD0");
                devices = await hueBridge.GetLightsAsync();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

        }

        public async void FindBridge()
        {
            hueBridge = await Bridge.FindAsync();
            hueBridge.UserId = "3TkCwQ2eiHdB4cp9cVgaEayWibS-AqrBZxLBhmD0";
        }

        public async Task<List<string>> GetDeviceNamesAsync()
        {
            devices = await hueBridge.GetLightsAsync();
            return devices.Select(device => device.Name).ToList();
        } 

    }
}
