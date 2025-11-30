namespace component
{
    public abstract class BaseComponent
    {
        private bool _isInitialized = false;

        public void Init()
        {
            _isInitialized = true;
        }

        protected abstract void OnUpdate();

        public void Update()
        {
            if (!_isInitialized)
            {
                return;
            }
            OnUpdate();
        }
    }
}