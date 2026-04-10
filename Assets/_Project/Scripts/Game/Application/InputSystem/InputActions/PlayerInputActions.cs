using UnityEngine.InputSystem;

namespace Game.Application.InputSystem.InputActions
{
    public class PlayerInputActions : InputActionsBase, InputSystemActions.IPlayerActions
    {
        public PlayerInputActions(IInputSystem inputSystem) : base(inputSystem)
        {
        }

        void InputSystemActions.IPlayerActions.OnMove(InputAction.CallbackContext context) { }

        void InputSystemActions.IPlayerActions.OnLook(InputAction.CallbackContext context) { }

        void InputSystemActions.IPlayerActions.OnAttack(InputAction.CallbackContext context) { }

        void InputSystemActions.IPlayerActions.OnInteract(InputAction.CallbackContext context) { }

        void InputSystemActions.IPlayerActions.OnCrouch(InputAction.CallbackContext context) { }

        void InputSystemActions.IPlayerActions.OnJump(InputAction.CallbackContext context) { }

        void InputSystemActions.IPlayerActions.OnPrevious(InputAction.CallbackContext context) { }

        void InputSystemActions.IPlayerActions.OnNext(InputAction.CallbackContext context) { }

        void InputSystemActions.IPlayerActions.OnSprint(InputAction.CallbackContext context) { }
    }
}