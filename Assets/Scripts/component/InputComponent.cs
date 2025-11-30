using entity;
using UnityEngine;
using UnityEngine.InputSystem;

namespace component
{
    public class InputComponent: BaseComponent
    {
        private InputSystem _inputSystem;

        public InputComponent(Entity entity) : base(entity)
        {
            _inputSystem = new InputSystem();
            _inputSystem.Player.Enable();
            _inputSystem.Player.Jump.performed += Jump;
        }

        private void Jump(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

            Debug.Log("Jump");
        }
    }
}