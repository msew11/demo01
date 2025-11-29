using system;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameConfig gameConfig;

    private PlayerMovementSystem _movementSystem;
    private GravitySystem _gravitySystem;
    private TppSystem _tppSystem;

    void Start()
    {
        _movementSystem = new PlayerMovementSystem(gameConfig, transform);
        _gravitySystem = new GravitySystem(gameConfig, transform);
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