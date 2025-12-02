using entity;
using eventbus;
using UnityEngine;
using UnityEngine.InputSystem;

namespace component
{
    public class InputComponent : BaseComponent
    {
        private InputSystem _inputSystem;

        public InputComponent(Entity entity) : base(entity)
        {
        }

        public override void Start()
        {
            _inputSystem = new InputSystem();
            _inputSystem.Player.Enable();
            _inputSystem.Player.Jump.performed += Jump;
            _inputSystem.Player.Move.performed += Move;
            _inputSystem.Player.Move.canceled += Move;
        }

        public override void OnDestroy()
        {
            _inputSystem.Player.Jump.performed -= Jump;
            _inputSystem.Player.Move.performed -= Move;
            _inputSystem.Player.Move.canceled -= Move;
            _inputSystem.Player.Disable();
            _inputSystem.Dispose();
        }

        public override void FixUpdate()
        {
            // var horizontal = Input.GetAxis("Horizontal");
            // var vertical = Input.GetAxis("Vertical");
            // Debug.Log($"{horizontal},{vertical}");
        }

        private void Jump(InputAction.CallbackContext context)
        {
            Entity.SendEvent(new JumpEvent());
        }

        private void Move(InputAction.CallbackContext context)
        {
            Entity.SendEvent(new MoveEvent(context.ReadValue<Vector2>()));
        }
    }
}