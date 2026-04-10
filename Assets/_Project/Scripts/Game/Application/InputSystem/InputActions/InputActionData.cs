using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Application.InputSystem.InputActions
{
    public struct InputActionData
    {
        public InputActionPhase Phase;
    }
    
    public struct InputVectorActionData
    {
        public InputActionPhase Phase;
        public Vector2 Vector;
    }
}