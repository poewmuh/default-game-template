using System.Collections.Generic;
using VContainer;

namespace Game.Application.InputSystem
{
    public class InputSwitcher : IInputSwitcher
    {
        [Inject] private IInputSystem _inputSystem;
        
        private readonly Dictionary<object, InputSwitchMode> _requests = new();
        
        private InputSwitchMode _current = InputSwitchMode.Player;
        
        void IInputSwitcher.RequestSwitch(object requester, InputSwitchMode mode)
        {
            _requests[requester] = mode;
            UpdateMode();
        }

        void IInputSwitcher.ReleaseSwitch(object requester)
        {
            _requests.Remove(requester);
            UpdateMode();
        }
        
        private void UpdateMode()
        {
            var priorityMode = GetPriorityMode();
            if (_current == priorityMode) return;

            _current = priorityMode;
            _inputSystem.SwitchMode(priorityMode);
        }

        private InputSwitchMode GetPriorityMode()
        {
            var max = InputSwitchMode.Player;
            foreach (var mode in _requests.Values)
                if (mode > max) max = mode;
            return max;
        }
    }
}