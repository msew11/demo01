using data;
using UnityEngine;

namespace system
{
    public class GravitySystem : GameSystem
    {
        private readonly GameConfig _config;
        private readonly CharacterController _characterController;

        private readonly Transform _groundCheck;

        private readonly PlayerData _playerData;

        public GravitySystem(GameConfig config, Transform player, PlayerData playerData)
        {
            _config = config;
            _characterController = player.GetComponent<CharacterController>();

            _groundCheck = player.Find("GroundCheck");
            _playerData = playerData;
        }

        protected override void Update()
        {
            _playerData.IsGround = Physics.CheckSphere(_groundCheck.position, _config.groundCheckRadius, _config.groundLayer);

            if (_playerData.IsGround && _playerData.Velocity.y < 0)
            {
                _playerData.Velocity.y = 0;
            }

            if (!_playerData.IsGround)
            {
                ApplyGravity();
            }
        }

        private void ApplyGravity()
        {
            _playerData.Velocity.y += _config.gravity * Time.deltaTime;
        }
    }

}