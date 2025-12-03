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
            _inputSystem.Player.Look.performed += Look;
            _inputSystem.Player.LockMouse.performed += LockMouse;
            _inputSystem.Player.LockMouse.canceled += UnLockMouse;
        }

        public override void OnDestroy()
        {
            _inputSystem.Player.Jump.performed -= Jump;
            _inputSystem.Player.Move.performed -= Move;
            _inputSystem.Player.Move.canceled -= Move;
            _inputSystem.Player.Look.performed -= Look;
            _inputSystem.Player.LockMouse.performed -= LockMouse;
            _inputSystem.Player.LockMouse.canceled -= UnLockMouse;
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

        private void Look(InputAction.CallbackContext context)
        {
            Entity.SendEvent(new LookEvent(context.ReadValue<Vector2>()));
        }

        private void LockMouse(InputAction.CallbackContext context)
        {
            if (Cursor.visible)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        private void UnLockMouse(InputAction.CallbackContext context)
        {
            if (!Cursor.visible)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
}