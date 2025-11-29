using UnityEngine;

namespace system
{
    public class GravitySystem : GameSystem
    {
        private readonly GameConfig _config;
        private readonly CharacterController _characterController;

        private readonly Transform _groundCheck;

        private Vector3 _velocity = Vector3.zero;
        private bool _isGround;

        public GravitySystem(GameConfig config, Transform player)
        {
            _config = config;
            _characterController = player.GetComponent<CharacterController>();

            _groundCheck = player.Find("GroundCheck");
        }

        protected override void Update()
        {
            _isGround = Physics.CheckSphere(_groundCheck.position, _config.groundCheckRadius, _config.groundLayer);

            if (_isGround && _velocity.y < 0)
            {
                _velocity.y = 0;
            }

            if (!_isGround)
            {
                ApplyGravity();
            }
        }

        private void ApplyGravity()
        {
            _velocity.y += _config.gravity * Time.deltaTime;
            _characterController.Move(_velocity * Time.deltaTime);
        }

        public Vector3 GetVelocity()
        {
            return _velocity;
        }
    }

}