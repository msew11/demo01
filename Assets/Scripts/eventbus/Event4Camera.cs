using UnityEngine;

namespace eventbus
{
    public class LookEvent : BaseEvent
    {
        public Vector2 Dir;

        public LookEvent(Vector2 dir)
        {
            Dir = dir;
        }
    }
}