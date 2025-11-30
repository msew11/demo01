using entity;

namespace component
{
    public abstract class BaseComponent
    {
        protected Entity Entity;

        protected BaseComponent(Entity entity)
        {
            Entity = entity;
        }

        public void Start()
        {
        }

        public void FixUpdate(float deltaTime)
        {
        }

        public void Update()
        {
        }

        public void OnDestroy()
        {
        }
    }
}