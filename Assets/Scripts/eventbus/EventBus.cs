using System;
using System.Collections.Generic;
using System.Reflection;
using component;
using entity;
using UnityEngine;

namespace eventbus
{
    public class EventBus
    {
        private static EventBus _instance;
        public static EventBus Instance => _instance ?? (_instance = new EventBus());

        // 存储组件类型、事件类型与处理方法的映射关系
        private readonly Dictionary<Type, Dictionary<Type, List<MethodInfo>>> _eventHandlers = 
            new Dictionary<Type, Dictionary<Type, List<MethodInfo>>>();
        
        // 记录已经处理过的组件类型，避免重复收集
        private readonly HashSet<Type> _processedComponentTypes = new HashSet<Type>();

        private EventBus()
        {
        }

        // 在游戏初始化时调用此方法收集所有组件的事件处理方法
        public void Initialize()
        {
            // 清空已处理记录，确保可以重新初始化
            _processedComponentTypes.Clear();
            
            // 获取所有已注册的组件类型
            foreach (EntityType entityType in Enum.GetValues(typeof(EntityType)))
            {
                var componentTypes = ComponentRegistry.GetRegisteredComponents(entityType);
                foreach (var componentType in componentTypes)
                {
                    // 避免重复处理相同的组件类型
                    if (!_processedComponentTypes.Contains(componentType))
                    {
                        CollectEventHandlers(componentType);
                        _processedComponentTypes.Add(componentType);
                    }
                }
            }
        }

        // 收集指定组件类型上的事件处理方法
        private void CollectEventHandlers(Type componentType)
        {
            var methods = componentType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var method in methods)
            {
                var attributes = method.GetCustomAttributes(typeof(EventListenAttribute), false);
                if (attributes.Length == 0) continue;

                foreach (EventListenAttribute attr in attributes)
                {
                    // 确保组件类型在字典中存在
                    if (!_eventHandlers.ContainsKey(componentType))
                    {
                        _eventHandlers[componentType] = new Dictionary<Type, List<MethodInfo>>();
                    }

                    // 确保事件类型在字典中存在
                    if (!_eventHandlers[componentType].ContainsKey(attr.EventType))
                    {
                        _eventHandlers[componentType][attr.EventType] = new List<MethodInfo>();
                    }

                    // 添加方法到映射关系中
                    _eventHandlers[componentType][attr.EventType].Add(method);
                    
                    // 打印每个注册的方法详情
                    Debug.Log($"[EventBus] Registered event handler: Component={componentType.Name}, Event={attr.EventType.Name}, Method={method.Name}");
                }
            }
        }

        // 发送事件到指定组件实例
        public void SendEventToComponent(BaseComponent component, BaseEvent ev)
        {
            var componentType = component.GetType();
            var eventType = ev.GetType();

            // 检查是否存在该组件类型的事件处理映射
            if (!_eventHandlers.ContainsKey(componentType)) return;

            // 检查是否存在该事件类型的处理方法
            if (!_eventHandlers[componentType].ContainsKey(eventType)) return;

            // 调用所有匹配的事件处理方法
            foreach (var method in _eventHandlers[componentType][eventType])
            {
                method.Invoke(component, new object[] { ev });
            }
            
            // 同时处理事件类型的基类
            foreach (var baseType in GetBaseTypes(eventType))
            {
                if (_eventHandlers[componentType].ContainsKey(baseType))
                {
                    foreach (var method in _eventHandlers[componentType][baseType])
                    {
                        method.Invoke(component, new object[] { ev });
                    }
                }
            }
        }

        private IEnumerable<Type> GetBaseTypes(Type type)
        {
            var baseType = type.BaseType;
            while (baseType != null && baseType != typeof(object))
            {
                yield return baseType;
                baseType = baseType.BaseType;
            }
        }
    }
}