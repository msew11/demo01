using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    private Camera _mainCamera;
    private CharacterController _characterController;
    private Transform _lookAt;

    public float speed = 10f;
    public float rotateSpeed = 10f;
    public float mouseSensitivity = 2.0f;  // 鼠标灵敏度

    // 添加鼠标控制状态
    private bool _isFaceControl = false;

    // 添加相机俯仰控制变量
    private float _cameraPitch = 0f;  // 相机垂直旋转角度

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
        // 检测鼠标右键按下/释放
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

        _isFaceControl = Input.GetMouseButton(1);

        // 只在激活状态下执行鼠标面向控制
        if (_isFaceControl)
        {
            RoleFace();
            CameraPitchControl();
        }

        RoleMove();
    }

    void RoleFace()
    {
        // 使用鼠标相对移动
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;

        // 绕Y轴旋转角色（水平转向）
        transform.Rotate(Vector3.up, mouseX * rotateSpeed);
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
        _lookAt.localRotation = Quaternion.Euler(_cameraPitch, 0, 0);
    }

    void RoleMove()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        var dir = (transform.forward * vertical + transform.right * horizontal) * (speed * Time.deltaTime);
        _characterController.Move(dir);
    }
}

