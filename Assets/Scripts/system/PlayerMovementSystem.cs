using UnityEngine;

namespace system
{
    public class PlayerMovementSystem : GameSystem
    {
        private readonly GameConfig _config;
        private readonly CharacterController _characterController;

        public PlayerMovementSystem(GameConfig config, CharacterController characterController)
        {
            _config = config;
            _characterController = characterController;
            IsInitialized = true;
        }

        private void RoleMove()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            var dir =
                (_characterController.transform.forward * vertical + _characterController.transform.right * horizontal)
                * (_config.speed * Time.deltaTime);
            _characterController.Move(dir);
        }

        protected override void Update()
        {
            RoleMove();
        }
    }
}