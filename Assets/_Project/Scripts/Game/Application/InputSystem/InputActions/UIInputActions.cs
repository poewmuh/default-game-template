using UnityEngine.InputSystem;

namespace Game.Application.InputSystem.InputActions
{
    public class UIInputActions : InputActionsBase, InputSystemActions.IUIActions
    {
        public UIInputActions(IInputSystem inputSystem) : base(inputSystem)
        {
        }

        void InputSystemActions.IUIActions.OnNavigate(InputAction.CallbackContext context) { }
        void InputSystemActions.IUIActions.OnTrackedDevicePosition(InputAction.CallbackContext context) { }

        void InputSystemActions.IUIActions.OnTrackedDeviceOrientation(InputAction.CallbackContext context) { }

        void InputSystemActions.IUIActions.OnSubmit(InputAction.CallbackContext context) { }

        void InputSystemActions.IUIActions.OnCancel(InputAction.CallbackContext context) { }

        void InputSystemActions.IUIActions.OnPoint(InputAction.CallbackContext context) { }

        void InputSystemActions.IUIActions.OnClick(InputAction.CallbackContext context) { }

        void InputSystemActions.IUIActions.OnRightClick(InputAction.CallbackContext context) { }

        void InputSystemActions.IUIActions.OnMiddleClick(InputAction.CallbackContext context) { }

        void InputSystemActions.IUIActions.OnScrollWheel(InputAction.CallbackContext context) { }
    }
}