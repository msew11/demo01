using UnityEngine;

namespace system
{
    public class TppSystem: GameSystem
    {
        private readonly GameConfig _config;

        private readonly Transform _player;
        private readonly Transform _lookAt;

        // 镜头状态
        private CameraState _cameraState = CameraState.None;

        // 添加相机俯仰控制变量
        private float _cameraPitch; // 相机垂直旋转角度
        private float _cameraYaw; // 相机水平旋转角度
        private float _roleYaw; // 角色水平旋转角度

        enum CameraState
        {
            None = 0,
            Right = 1,
            Left = 2
        }

        public TppSystem(GameConfig config, Transform player)
        {
            _config = config;
            _player = player;
            _lookAt = player.Find("LookAt");
        }

        protected override void Update()
        {
            // 鼠标锁定控制
            HandleCursorLock();

            // 状态切换处理
            HandleStateSwitch();

            // 根据状态执行对应逻辑
            ExecuteCurrentState();
        }

        void HandleCursorLock()
        {
            if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }

        void HandleStateSwitch()
        {
            if (Input.GetMouseButton(1))
            {
                SwitchToRightMouseState();
            }
            else if (Input.GetMouseButton(0))
            {
                SwitchToLeftMouseState();
            }
            else
            {
                _cameraState = CameraState.None;
            }
        }

        void SwitchToRightMouseState()
        {
            if (_cameraState != CameraState.Right)
            {
                // 保存当前LookAt的正前方方向
                Vector3 lookDirection = _lookAt.forward;

                // 将角色朝向该方向（只取水平方向）
                _player.rotation = Quaternion.LookRotation(new Vector3(lookDirection.x, 0, lookDirection.z));

                // 重置LookAt的本地旋转，使其与角色保持一致
                _lookAt.localRotation = Quaternion.identity;

                // 同步yaw值
                _roleYaw = _player.eulerAngles.y;

                // 重置相机水平角度，保持俯仰角度不变
                _cameraYaw = 0f;

                // 重新应用当前的俯仰角到LookAt
                _lookAt.localRotation = Quaternion.Euler(_cameraPitch, _cameraYaw, 0);
            }
            _cameraState = CameraState.Right;
        }

        void SwitchToLeftMouseState()
        {
            if (_cameraState != CameraState.Left)
            {
            }
            _cameraState = CameraState.Left;
        }

        void ExecuteCurrentState()
        {
            if (_cameraState == CameraState.Right)
            {
                // 右键模式
                RightMouseButtonState();
            }
            else if (_cameraState == CameraState.Left)
            {
                // 左键模式
                LeftMouseButtonState();
            }
        }

        void LeftMouseButtonState()
        {
            LookAtControl();
            CameraPitchControl();
        }

        void RightMouseButtonState()
        {
            RoleFaceControl();
            CameraPitchControl();
        }

        void RoleFaceControl()
        {
            // 使用鼠标相对移动
            float mouseX = Input.GetAxis("Mouse X") * _config.mouseSensitivity;

            // 累加水平角度
            _roleYaw += mouseX;

            // 合并水平和垂直旋转
            _player.localRotation = Quaternion.Euler(0, _roleYaw, 0);
        }

        void LookAtControl()
        {
            // 使用鼠标相对移动
            float mouseX = Input.GetAxis("Mouse X") * _config.mouseSensitivity;

            // 累加水平角度
            _cameraYaw += mouseX;

            // 合并水平和垂直旋转
            _lookAt.localRotation = Quaternion.Euler(_cameraPitch, _cameraYaw, 0);
        }

        void CameraPitchControl()
        {
            // 获取鼠标Y轴移动
            float mouseY = Input.GetAxis("Mouse Y");

            // 累加俯仰角度
            _cameraPitch -= mouseY;

            // 限制俯仰角度范围（避免过度旋转）
            _cameraPitch = Mathf.Clamp(_cameraPitch, -20f, 70f);

            // 应用旋转到LookAt子物体
            _lookAt.localRotation = Quaternion.Euler(_cameraPitch, _cameraYaw, 0);
        }
    }
}