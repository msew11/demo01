using System.Collections.Generic;

namespace entity
{
    /// <summary>
    /// 实体注册初始化器，在游戏启动时注册各种实体类型需要的组件
    /// </summary>
    public static class EntityRegistryInitializer
    {
        public static void Initialize()
        {
            ComponentRegistry.RegisterComponents(EntityType.Role, new List<System.Type>
            {
                typeof(component.InputComponent),
                typeof(component.MovementComponent)
            });
        }
    }
}