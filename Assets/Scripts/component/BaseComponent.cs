using entity;
using eventbus;

namespace component
{
    public abstract class BaseComponent
    {
        protected Entity Entity;

        protected BaseComponent(Entity entity)
        {
            Entity = entity;
        }

        public virtual void Start()
        {
        }

        public virtual void FixUpdate(float deltaTime)
        {
        }

        public virtual void Update()
        {
        }

        public virtual void OnDestroy()
        {
        }
        
        public void SendEvent(BaseEvent ev)
        {
            EventBus.Instance.SendEventToComponent(this, ev);
        }
    }
}