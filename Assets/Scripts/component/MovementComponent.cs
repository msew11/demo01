using data;
using entity;
using eventbus;
using UnityEngine;

namespace component
{
    public class MovementComponent : BaseComponent
    {
        private MoveData _moveData;

        private CharacterController _characterController;

        public MovementComponent(Entity entity) : base(entity)
        {
        }

        public override void Start()
        {
            _characterController = Entity.Transform.GetComponent<CharacterController>();
            if (!_characterController)
            {
                Debug.LogError($"MovementComponent Entity[{Entity.Id}] 未找到组件CharacterController");
                return;
            }

            _moveData = Entity.GetData<MoveData>();
        }

        public override void FixUpdate()
        {
            _characterController.Move(_moveData.UpVelocity * Time.deltaTime);
            if (_moveData.Dir != Vector3.zero)
            {
                _characterController.Move(_moveData.Dir * (Time.deltaTime * Game.Instance.RunParam.speed));
            }
        }

        [EventListen(typeof(JumpEvent))]
        public void OnJump(JumpEvent ev)
        {
            if (_moveData.IsGround)
            {
                _moveData.UpVelocity.y += Mathf.Sqrt(Game.Instance.RunParam.jumpHeight * -2f * Game.Instance.RunParam.gravity);
            }
        }

        [EventListen(typeof(MoveEvent))]
        public void OnMove(MoveEvent ev)
        {
            Debug.Log($"OnMove {ev.Dir}");
            _moveData.Dir = Entity.Transform.forward * ev.Dir.y + Entity.Transform.right * ev.Dir.x;
        }
    }
}