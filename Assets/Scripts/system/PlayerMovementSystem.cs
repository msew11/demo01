using data;
using UnityEngine;

namespace system
{
    public class PlayerMovementSystem : GameSystem
    {
        private readonly GameConfig _config;
        private readonly Transform _player;
        private readonly CharacterController _characterController;

        private readonly PlayerData _playerData;

        public PlayerMovementSystem(GameConfig config, Transform player, PlayerData playerData)
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
            _characterController.Move(_playerData.Velocity * Time.deltaTime);
        }

        private void Jump()
        {
            if (_playerData.IsGround && Input.GetButtonDown("Jump"))
            {
                _playerData.Velocity.y += Mathf.Sqrt(_config.jumpHeight * -2f * _config.gravity);
            }
        }

        protected override void Update()
        {
            Move();
            Jump();
        }
    }
}