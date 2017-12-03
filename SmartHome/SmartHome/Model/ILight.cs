namespace SmartHome.Model
{
    public interface ILight
    {
        string Name { get; }

        void ChangeColorCoordinates(double x, double y);
        void ChangeBrightness(byte value);
        void ChangeSaturation(byte value);
        void ChangeHue(ushort value);

        void SwitchOn();
        void SwitchOff();

        bool IsOn();
    }
}