using data;
using entity;
using eventbus;
using UnityEngine;

namespace component
{
    public class CameraTppComponent : BaseComponent
    {
        private Transform _lookRoot;
        private MoveData _moveData;

        public CameraTppComponent(Entity entity) : base(entity)
        {
        }

        public override void Start()
        {
            _lookRoot = Entity.Transform.Find("LookRoot");
            if (!_lookRoot)
            {
                Debug.LogError($"CameraTppComponent Entity[{Entity.Id}] 未设定LookRoot");
            }
            _moveData = Entity.GetData<MoveData>();
        }

        public override void FixUpdate()
        {
            if (_lookRoot)
            {
                _lookRoot.localRotation = Quaternion.Euler(_moveData.CameraPitch, _moveData.CameraYaw, 0);
            }
        }


        [EventListen(typeof(LookEvent))]
        public void OnLook(LookEvent ev)
        {
            _moveData.RoleYaw += ev.Dir.x * Game.Instance.Setting.mouseXSensitivity;
            _moveData.CameraPitch -= ev.Dir.y * Game.Instance.Setting.mouseYSensitivity;
            _moveData.CameraPitch = Mathf.Clamp(_moveData.CameraPitch, -20f, 70f);
            Entity.Transform.localRotation = Quaternion.Euler(0, _moveData.RoleYaw, 0);
        }
    }
}