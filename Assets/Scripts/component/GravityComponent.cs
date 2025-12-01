using data;
using entity;
using UnityEngine;

namespace component
{
    public class GravityComponent : BaseComponent
    {
        private Transform _groundCheck;
        private MoveData _moveData;

        public GravityComponent(Entity entity) : base(entity)
        {
        }

        public override void Start()
        {
            _groundCheck = Entity.Transform.Find("GroundCheck");
            if (!_groundCheck)
            {
                Debug.LogError($"GravityComponent Entity[{Entity.Id}] 未设定GroundCheck");
                return;
            }

            _moveData = Entity.GetData<MoveData>();
        }

        public override void FixUpdate()
        {
            if (!_groundCheck)
            {
                return;
            }

            _moveData.IsGround = Physics.CheckSphere(
                _groundCheck.position,
                Game.Instance.RunParam.groundCheckRadius,
                Game.Instance.RunParam.groundLayer
            );

            if (_moveData.IsGround && _moveData.UpVelocity.y < 0)
            {
                _moveData.UpVelocity.y = 0;
            }

            if (!_moveData.IsGround)
            {
                ApplyGravity();
            }
        }

        private void ApplyGravity()
        {
            _moveData.UpVelocity.y += Game.Instance.RunParam.gravity * Time.deltaTime;
        }
    }
}