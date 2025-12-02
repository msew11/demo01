using UnityEngine;

namespace eventbus
{
    public class JumpEvent: BaseEvent
    {

    }

    public class MoveEvent: BaseEvent
    {
        public Vector2 Dir;
        public MoveEvent(Vector2 dir)
        {
            Dir = dir;
        }
    }
}