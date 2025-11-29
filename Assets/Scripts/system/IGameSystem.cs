namespace system
{
    public abstract class GameSystem
    {
        protected bool IsInitialized = false;

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