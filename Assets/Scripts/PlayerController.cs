using data;
using system;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameConfig gameConfig;

    private PlayerMovementSystem _movementSystem;
    private GravitySystem _gravitySystem;
    private TppSystem _tppSystem;

    private PlayerData _playerData;

    void Awake()
    {
        // 初始化数据
        var dataManager = DataManager.Instance;
        _playerData = new PlayerData(dataManager.LocalPlayerData.PlayerId);
        dataManager.AddPlayerData(_playerData);
    }

    void Start()
    {
        _movementSystem = new PlayerMovementSystem(gameConfig, transform, _playerData);
        _gravitySystem = new GravitySystem(gameConfig, transform, _playerData);
        _tppSystem = new TppSystem(gameConfig, transform);

        _movementSystem.Init();
        _gravitySystem.Init();
        _tppSystem.Init();
    }

    void Update()
    {
        _movementSystem.HandleUpdate();
        _gravitySystem.HandleUpdate();
        _tppSystem.HandleUpdate();
    }
}