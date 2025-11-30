using entity;
using eventbus;
using UnityEngine;

namespace component
{
    public class MovementComponent : BaseComponent
    {
        public MovementComponent(Entity entity) : base(entity)
        {
        }

        [EventListen(typeof(JumpEvent))]
        public void OnJump(JumpEvent ev)
        {
            Debug.Log("OnJump");
        }
    }
}