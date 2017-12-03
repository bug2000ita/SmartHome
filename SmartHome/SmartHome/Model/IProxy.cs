using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using HueLibrary;

namespace SmartHome.Model
{
    public interface IProxy
    {
        IEnumerable DeviceNames { get; }
        void Connect(string ip);
        void FindBridge();
        ILight GetLightByName(string name);

        Task<List<string>> GetDeviceNamesAsync();

    }
}