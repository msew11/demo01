using data;
using entity;
using eventbus;
using UnityEngine;

namespace component
{
    public class CameraTppComponent : BaseComponent
    {
        private MoveData _moveData;

        public CameraTppComponent(Entity entity) : base(entity)
        {
        }

        public override void Start()
        {
            _moveData = Entity.GetData<MoveData>();
        }

        public override void FixUpdate()
        {
        }


        [EventListen(typeof(LookEvent))]
        public void OnLook(LookEvent ev)
        {
            _moveData.RoleYaw += ev.Dir.x * Game.Instance.Setting.mouseSensitivity;
            Entity.Transform.localRotation = Quaternion.Euler(0, _moveData.RoleYaw, 0);
        }
    }
}