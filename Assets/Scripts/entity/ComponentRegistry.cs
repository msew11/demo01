using System;
using System.Collections.Generic;
using component;

namespace entity
{
    /// <summary>
    /// 组件注册表，用于注册每种EntityType需要的组件类型
    /// </summary>
    public static class ComponentRegistry
    {
        private static readonly Dictionary<EntityType, List<Type>> ComponentRegistrations = new();

        /// <summary>
        /// 注册EntityType所需的组件类型
        /// </summary>
        /// <param name="entityType">实体类型</param>
        /// <param name="componentType">组件类型</param>
        private static void RegisterComponent(EntityType entityType, Type componentType)
        {
            // 验证componentType是否继承自BaseComponent
            if (!typeof(BaseComponent).IsAssignableFrom(componentType))
            {
                throw new ArgumentException($"Type {componentType.Name} must inherit from BaseComponent", nameof(componentType));
            }

            // 验证componentType是否有无参构造函数
            var ctor = componentType.GetConstructor(new[] { typeof(Entity) });
            if (ctor == null)
            {
                throw new ArgumentException($"Component type {componentType.Name} must have a parameterless constructor", nameof(componentType));
            }

            if (!ComponentRegistrations.ContainsKey(entityType))
            {
                ComponentRegistrations[entityType] = new List<Type>();
            }

            ComponentRegistrations[entityType].Add(componentType);
        }

        /// <summary>
        /// 注册EntityType所需的多个组件类型
        /// </summary>
        /// <param name="entityType">实体类型</param>
        /// <param name="componentTypes">组件类型列表</param>
        public static void RegisterComponents(EntityType entityType, IEnumerable<Type> componentTypes)
        {
            foreach (var componentType in componentTypes)
            {
                RegisterComponent(entityType, componentType);
            }
        }

        /// <summary>
        /// 获取指定EntityType注册的所有组件类型
        /// </summary>
        /// <param name="entityType">实体类型</param>
        /// <returns>组件类型列表</returns>
        public static IReadOnlyList<Type> GetRegisteredComponents(EntityType entityType)
        {
            if (ComponentRegistrations.TryGetValue(entityType, out var components))
            {
                return components.AsReadOnly();
            }

            return new List<Type>().AsReadOnly();
        }

        /// <summary>
        /// 清除所有注册信息
        /// </summary>
        public static void Clear()
        {
            ComponentRegistrations.Clear();
        }
    }
}