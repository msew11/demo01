namespace system
{
    public abstract class GameSystem
    {
        protected bool IsInitialized = false;

        public void Init()
        {
            IsInitialized = true;
        }

        protected abstract void Update();

        public void HandleUpdate()
        {
            if (!IsInitialized)
            {
                return;
            }
            Update();
        }
    }
}