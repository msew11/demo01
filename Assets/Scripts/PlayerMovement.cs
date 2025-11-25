using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Camera _mainCamera;
    private CharacterController _characterController;

    public float speed = 10f;
    public float rotateSpeed = 10f;
    public float mouseSensitivity = 2.0f;  // 鼠标灵敏度

    // 添加鼠标控制状态
    private bool _isMouseControlActive = false;

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
    }

    void Update()
    {
        // 检测鼠标右键按下/释放
        if (Input.GetMouseButtonDown(1))
        {
            StartMouseControl();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            StopMouseControl();
        }

        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        var dir = (transform.forward * vertical + transform.right * horizontal) * (speed * Time.deltaTime);
        _characterController.Move(dir);

        // 只在激活状态下执行鼠标面向控制
        if (_isMouseControlActive)
        {
            FaceMouse();
        }
    }

    void StartMouseControl()
    {
        _isMouseControlActive = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void StopMouseControl()
    {
        _isMouseControlActive = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void FaceMouse()
    {
        // 使用鼠标相对移动
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // 绕Y轴旋转角色（水平转向）
        transform.Rotate(Vector3.up, mouseX * rotateSpeed);

        // 控制相机垂直旋转（俯仰）
        _cameraPitch -= mouseY;  // 反向是因为鼠标Y轴是反的
        _cameraPitch = Mathf.Clamp(_cameraPitch, -80f, 80f);  // 限制俯仰角度

        // 应用相机旋转到相机
        if (_mainCamera != null)
        {

            Debug.Log($"{mouseY}");
            // 保存当前相机的Y轴旋转（来自角色）
            Vector3 currentRotation = _mainCamera.transform.eulerAngles;
            // 只修改X轴（俯仰）和保持Y轴跟随角色
            _mainCamera.transform.rotation = Quaternion.Euler(_cameraPitch, currentRotation.y, currentRotation.z);
        }
    }
}

