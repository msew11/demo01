using component;
using data;
using UnityEngine;
using UnityEngine.InputSystem;

/**
 * 使用新版InputSystem
 */
public class PlayerControllerV2 : MonoBehaviour
{
    [SerializeField] GameConfig gameConfig;

    private InputSystem _inputSystem;

    private PlayerMovementComponent _movementComponent;
    private GravityComponent _gravityComponent;

    private PlayerData _playerData;

    void Awake()
    {
        Debug.Log($"PlayerControllerV2 Awake - Component enabled: {enabled}");

        _inputSystem = new InputSystem();
        _inputSystem.Player.Enable();
        _inputSystem.Player.Jump.performed += Jump;
    }

    void Start()
    {
        // 初始化数据
        var dataManager = DataManager.Instance;
        _playerData = new PlayerData(dataManager.LocalPlayerData.PlayerId);
        dataManager.AddPlayerData(_playerData);

        _movementComponent = new PlayerMovementComponent(gameConfig, transform, _playerData);
        _gravityComponent = new GravityComponent(gameConfig, transform, _playerData);
        _movementComponent.Init();
        _gravityComponent.Init();
    }

    void Update()
    {
        _movementComponent.Update();
        _gravityComponent.Update();
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        Debug.Log("Jump");
        _movementComponent.Jump();
    }
}