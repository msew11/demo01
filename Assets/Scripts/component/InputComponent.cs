using entity;
using eventbus;
using UnityEngine;
using UnityEngine.InputSystem;

namespace component
{
    public class InputComponent: BaseComponent
    {
        private readonly InputSystem _inputSystem;

        public InputComponent(Entity entity) : base(entity)
        {
            _inputSystem = new InputSystem();
            _inputSystem.Player.Enable();
            _inputSystem.Player.Jump.performed += Jump;
        }

        public override void OnDestroy()
        {
            _inputSystem.Player.Jump.performed -= Jump;
            _inputSystem.Player.Disable();
            _inputSystem.Dispose();
        }

        private void Jump(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

            Debug.Log("Input Jump");
            Entity.SendEvent(new JumpEvent());
        }
    }
}