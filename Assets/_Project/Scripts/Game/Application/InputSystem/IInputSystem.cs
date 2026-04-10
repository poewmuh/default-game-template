using R3;
using UnityEngine.InputSystem;

namespace Game.Application.InputSystem
{
    public interface IInputSystem
    {
        PlayerInput PlayerInput { get; }
        InputSystemActions InputSystemActions { get; }
        ReactiveProperty<ControlSchemeType> ControlScheme { get; }
        void SwitchMode(InputSwitchMode mode);
    }
}