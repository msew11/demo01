using entity;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public long entityId;

    private Entity _player;

    void Start()
    {
        Debug.Log($"PlayerController Start: entity[{entityId}]");
        _player = Game.Instance.GetEntity(entityId);
        _player.Start();
    }

    void FixedUpdate()
    {
        _player.FixUpdate();
    }

    void Update()
    {
        _player.Update();
    }

    private void OnDestroy()
    {
        _player.OnDestroy();
    }
}