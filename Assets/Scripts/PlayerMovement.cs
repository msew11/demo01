using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Camera _mainCamera;
    private CharacterController _characterController;
    private Transform _lookAt;

    public float speed = 10f;
    public float rotateSpeed = 10f;
    public float mouseSensitivity = 2.0f; // 鼠标灵敏度

    // 镜头状态
    private CameraState _cameraState = CameraState.NONE;

    // 添加相机俯仰控制变量
    private float _cameraPitch = 0f; // 相机垂直旋转角度
    private float _cameraYaw = 0f; // 相机水平旋转角度
    private float _roleYaw = 0f; // 角色水平旋转角度

    enum CameraState
    {
        NONE = 0,
        RIGHT = 1,
        LEFT = 2
    }

    void Start()
    {
        _mainCamera = Camera.main;
        if (_mainCamera == null)
        {
            Debug.LogError("未找到主相机");
            return;
        }

        _characterController = GetComponent<CharacterController>();

        _lookAt = transform.Find("LookAt");
    }

    void Update()
    {
        // 鼠标锁定控制
        HandleCursorLock();

        // 状态切换处理
        HandleStateSwitch();

        // 根据状态执行对应逻辑
        ExecuteCurrentState();

        // 角色移动
        RoleMove();
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
            _cameraState = CameraState.NONE;
        }
    }

    void SwitchToRightMouseState()
    {
        if (_cameraState != CameraState.RIGHT)
        {
            // 保存当前LookAt的正前方方向
            Vector3 lookDirection = _lookAt.forward;
            // 将角色朝向该方向（只取水平方向）
            transform.rotation = Quaternion.LookRotation(new Vector3(lookDirection.x, 0, lookDirection.z));

            // 重置LookAt的本地旋转，使其与角色保持一致
            _lookAt.localRotation = Quaternion.identity;

            // 同步yaw值
            _roleYaw = transform.eulerAngles.y;
            // 重置相机水平角度，保持俯仰角度不变
            _cameraYaw = 0f;

            // 重新应用当前的俯仰角到LookAt
            _lookAt.localRotation = Quaternion.Euler(_cameraPitch, _cameraYaw, 0);
        }
        _cameraState = CameraState.RIGHT;
    }

    void SwitchToLeftMouseState()
    {
        if (_cameraState != CameraState.LEFT)
        {
        }
        _cameraState = CameraState.LEFT;
    }

    void ExecuteCurrentState()
    {
        if (_cameraState == CameraState.RIGHT)
        {
            // 右键模式
            RightMouseButtonState();
        }
        else if (_cameraState == CameraState.LEFT)
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
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;

        // 累加水平角度
        _roleYaw += mouseX * rotateSpeed;

        // 合并水平和垂直旋转
        transform.localRotation = Quaternion.Euler(0, _roleYaw, 0);
    }

    void LookAtControl()
    {
        // 使用鼠标相对移动
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;

        // 累加水平角度
        _cameraYaw += mouseX * rotateSpeed;

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
        _cameraPitch = Mathf.Clamp(_cameraPitch, -90f, 90f);

        // 应用旋转到LookAt子物体
        _lookAt.localRotation = Quaternion.Euler(_cameraPitch, _cameraYaw, 0);
    }

    void RoleMove()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        var dir = (transform.forward * vertical + transform.right * horizontal) * (speed * Time.deltaTime);
        _characterController.Move(dir);
    }
}