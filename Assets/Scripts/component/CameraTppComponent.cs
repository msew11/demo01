using entity;
using eventbus;
using UnityEngine;

namespace component
{
    public class CameraTppComponent : BaseComponent
    {
        public CameraTppComponent(Entity entity) : base(entity)
        {
        }

        public override void Start()
        {
        }

        public override void FixUpdate()
        {
        }

        private float _roleYaw; // 角色水平旋转角度

        [EventListen(typeof(LookEvent))]
        public void OnLook(LookEvent ev)
        {
            _roleYaw += ev.Dir.x * Game.Instance.Setting.mouseSensitivity;
            Entity.Transform.localRotation = Quaternion.Euler(0, _roleYaw, 0);
        }
    }
}