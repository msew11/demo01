using data;
using entity;
using eventbus;
using UnityEngine;

namespace component
{
    public class MovementComponent : BaseComponent
    {
        private readonly MoveData _moveData;

        public MovementComponent(Entity entity) : base(entity)
        {
            _moveData = Entity.GetData<MoveData>();
        }

        [EventListen(typeof(JumpEvent))]
        public void OnJump(JumpEvent ev)
        {
            if (_moveData.IsGround)
            {
                _moveData.Velocity.y += Mathf.Sqrt(Game.Instance.RunParam.jumpHeight * -2f * Game.Instance.RunParam.gravity);
            }
        }
    }
}