using System;
using UnityEngine;

namespace entity
{
    public abstract class EntityFactory
    {
        private static long _nextId = 1;

        public static Entity CreateEntity(EntityType entityType, GameObject obj)
        {
            // 生成唯一ID
            long id = _nextId++;

            // 创建实体并传入ID
            Entity entity = new Entity(id, obj.transform);

            // 根据注册表自动添加组件
            var registeredComponents = ComponentRegistry.GetRegisteredComponents(entityType);
            foreach (var componentType in registeredComponents)
            {
                // 直接调用Entity的AddComponent方法添加组件
                entity.AddComponent(componentType);
            }

            Game.Instance.AddEntity(entity);

            switch (entityType)
            {
                case EntityType.Role:
                    return entity;
                default:
                    throw new ArgumentException($"Unsupported entity type: {entityType}");
            }
        }
    }
}