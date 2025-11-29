using system;
using UnityEngine;

public class PlayerController: MonoBehaviour
{
    [SerializeField]
    GameConfig gameConfig;

    private PlayerMovementSystem _movementSystem;
    private TppSystem _tppSystem;

    void Start()
    {
        CharacterController characterController = GetComponent<CharacterController>();
        if (gameConfig == null)
        {
            Debug.LogError("未找到游戏配置");
            return;
        }

        Transform lookAt = transform.Find("LookAt");
        if (lookAt == null)
        {
            Debug.LogError("未找到lookAt");
            return;
        }

        _movementSystem = new PlayerMovementSystem(gameConfig, characterController);
        _tppSystem = new TppSystem(gameConfig, transform, lookAt);
    }

    void Update()
    {
        _movementSystem.HandleUpdate();
        _tppSystem.HandleUpdate();
    }
}