using System.Collections.Generic;
using data;
using entity;

public class Game
{
    public static Game Instance { get; } = new Game();

    public PlayerConfig PlayerConfig;
    public LocalPlayerData LocalPlayerData;
    private readonly Dictionary<long, Entity> _entityMap;

    private Game()
    {
        _entityMap = new Dictionary<long, Entity>();
    }

    // 添加Entity
    public void AddEntity(Entity entity)
    {
        _entityMap[entity.Id] = entity;
    }

    // 获取Entity
    public Entity GetEntity(long entityId)
    {
        return _entityMap.GetValueOrDefault(entityId);
    }

    // 移除Entity
    public void RemoveEntity(long entityId)
    {
        _entityMap.Remove(entityId);
    }
}