using System.Collections.Generic;
using component;
using data;
using System;
using eventbus;
using System.Reflection;

namespace entity
{
    public class Entity
    {
        private readonly Dictionary<string, BaseData> _dataMap = new();
        private readonly Dictionary<string, BaseComponent> _componentMap = new();
        private static readonly Dictionary<Type, ConstructorInfo> ConstructorCache = new();

        public Entity(long id)
        {
            Id = id;
        }

        public long Id { get; }

        public void Start()
        {
            foreach (var component in _componentMap.Values)
            {
                component.Start();
            }
        }

        public void FixUpdate(float deltaTime)
        {
            foreach (var component in _componentMap.Values)
            {
                component.FixUpdate(deltaTime);
            }
        }

        public void Update()
        {
            foreach (var component in _componentMap.Values)
            {
                component.Update();
            }
        }

        public void OnDestroy()
        {
            foreach (var component in _componentMap.Values)
            {
                component.OnDestroy();
            }
        }

        public BaseComponent AddComponent(Type componentType)
        {
            // 检查类型是否继承自BaseComponent
            if (!typeof(BaseComponent).IsAssignableFrom(componentType))
            {
                throw new ArgumentException($"Type {componentType.Name} must inherit from BaseComponent", nameof(componentType));
            }

            // 获取带Entity参数的构造函数（带缓存）
            if (!ConstructorCache.TryGetValue(componentType, out var ctor))
            {
                ctor = componentType.GetConstructor(new[] { typeof(Entity) });
                if (ctor == null)
                {
                    throw new ArgumentException($"Component type {componentType.Name} must have a constructor with Entity parameter", nameof(componentType));
                }
                ConstructorCache[componentType] = ctor;
            }

            // 使用带Entity参数的构造函数创建实例
            var component = (BaseComponent)ctor.Invoke(new object[] { this });

            var componentName = componentType.Name;
            _componentMap[componentName] = component;
            return component;
        }

        public T GetData<T>() where T : BaseData, new()
        {
            var dataName = typeof(T).Name;
            if (!_dataMap.TryGetValue(dataName, out var data))
            {
                // 当数据不存在时，初始化一个新的数据实例
                data = new T();
                _dataMap[dataName] = data;
            }
            return data as T;
        }

        public void SendEvent(BaseEvent ev)
        {
            foreach (var component in _componentMap.Values)
            {
                component.SendEvent(ev);
            }
        }
    }
}