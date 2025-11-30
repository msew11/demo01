using System;
using data;
using entity;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public long entityId;

    private Entity _player;
    private MoveData _moveData;

    private CharacterController _characterController;

    void Awake()
    {
        Debug.Log($"PlayerController Awake: entity[{entityId}]");
        _characterController = GetComponent<CharacterController>();
    }

    void Start()
    {
        Debug.Log($"PlayerController Start: entity[{entityId}]");
        _player = Game.Instance.GetEntity(entityId);
        _moveData = _player.GetData<MoveData>();
        _player.Start();
    }

    void FixedUpdate()
    {
        _player.FixUpdate(Time.deltaTime);

        if (_moveData.IsDirty)
        {
            _characterController.Move(_moveData.Dir);
            _moveData.IsDirty = false;
        }

        _moveData.Position = transform.position;

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