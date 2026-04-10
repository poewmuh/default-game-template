namespace Game.Application.InputSystem
{
    public interface IInputSwitcher
    {
        void RequestSwitch(object requester, InputSwitchMode mode);
        void ReleaseSwitch(object requester);
    }
}