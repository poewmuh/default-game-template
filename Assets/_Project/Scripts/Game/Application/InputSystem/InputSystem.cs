using Game.Application.InputSystem.InputActions;
using R3;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

namespace Game.Application.InputSystem
{
    public class InputSystem : MonoBehaviour, IInputSystem
    {
        [SerializeField] private PlayerInput _playerInput;
        
        private InputSystemActions _inputActions;
        private UIInputActions _uiInputActions;
        private PlayerInputActions _playerInputActions;

        InputSystemActions IInputSystem.InputSystemActions => _inputActions;
        PlayerInput IInputSystem.PlayerInput => _playerInput;

        public ReactiveProperty<ControlSchemeType> ControlScheme { get; } = new();

        private void Start()
        {
            _inputActions = new InputSystemActions();
            
            _uiInputActions = new UIInputActions(this);
            _playerInputActions = new PlayerInputActions(this);
            
            _inputActions.Player.SetCallbacks(_playerInputActions);
            _inputActions.UI.SetCallbacks(_uiInputActions);
            
            _playerInput.actions = _inputActions.asset;
            OnControlChanged(_playerInput);
            _playerInput.onControlsChanged += OnControlChanged;
            
            var uiModule = EventSystem.current.GetComponent<InputSystemUIInputModule>();
            uiModule.actionsAsset = _inputActions.asset;
        }

        private void OnDestroy()
        {
            _playerInput.onControlsChanged -= OnControlChanged;
        }

        void IInputSystem.SwitchMode(InputSwitchMode mode)
        {
            _inputActions.Player.Disable();
            _inputActions.UI.Disable();

            switch (mode)
            {
                case InputSwitchMode.None:
                    EventSystem.current.SetSelectedGameObject(null);
                    break;
                case InputSwitchMode.UI:
                    _inputActions.UI.Enable();
                    break;
                case InputSwitchMode.Player:
                    _inputActions.Player.Enable();
                    EventSystem.current.SetSelectedGameObject(null);
                    break;
            }
            
            Debug.Log($"[InputSystem] Input Mode Switched To: {mode}");
        }

        private void OnControlChanged(PlayerInput playerInput)
        {
            if (playerInput.currentControlScheme.Contains("Gamepad"))
            {
                ControlScheme.Value = ControlSchemeType.Gamepad;
            }
            else if (playerInput.currentControlScheme.Contains("Mouse"))
            {
                ControlScheme.Value = ControlSchemeType.KeyboardMouse;
            }
            else ControlScheme.Value = ControlSchemeType.Other;
            
            Debug.Log($"[InputSystem] Control Scheme Changed To: {ControlScheme.Value}");
        }
    }
}