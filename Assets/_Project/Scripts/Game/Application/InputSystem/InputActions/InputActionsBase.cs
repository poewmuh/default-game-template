using R3;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Application.InputSystem.InputActions
{
    public abstract class InputActionsBase
    {
        protected readonly IInputSystem InputSystem;
        
        protected InputActionsBase(IInputSystem inputSystem)
        {
            InputSystem = inputSystem;
        }
        
        protected static void TriggerAction(InputAction.CallbackContext context, Subject<InputActionData> subject, bool requirePerformed = true)
        {
            if (requirePerformed && !context.performed) return;
            
            subject?.OnNext(new InputActionData
            {
                Phase = context.phase
            });
        }
        
        protected static void TriggerAction(InputAction.CallbackContext context, Subject<InputVectorActionData> subject)
        {
            subject?.OnNext(new InputVectorActionData
            {
                Phase = context.phase,
                Vector = context.action.ReadValue<Vector2>()
            });
        }
    }
}