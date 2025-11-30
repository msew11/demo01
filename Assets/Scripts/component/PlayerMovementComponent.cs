using data;
using UnityEngine;

namespace component
{
    public class PlayerMovementComponent : BaseComponent
    {
        private readonly GameConfig _config;
        private readonly Transform _player;
        private readonly CharacterController _characterController;

        private readonly PlayerData _playerData;

        public PlayerMovementComponent(GameConfig config, Transform player, PlayerData playerData)
        {
            _config = config;
            _player = player;
            _characterController = player.GetComponent<CharacterController>();
            _playerData = playerData;
        }

        private void Move()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            var dir = (_player.forward * vertical + _player.right * horizontal) * (_config.speed * Time.deltaTime);
            _characterController.Move(dir);
        }

        public void Jump()
        {
            if (_playerData.IsGround)
            {
                _playerData.Velocity.y += Mathf.Sqrt(_config.jumpHeight * -2f * _config.gravity);
            }
        }

        protected override void OnUpdate()
        {
            Move();
            _characterController.Move(_playerData.Velocity * Time.deltaTime);
        }
    }
}