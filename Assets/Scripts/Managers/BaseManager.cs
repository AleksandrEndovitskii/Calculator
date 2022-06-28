namespace Managers
{
    public abstract class BaseManager<T> : InitializableBaseMonoBehaviour where T : InitializableBaseMonoBehaviour
    {
        public static T Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this.gameObject.GetComponent<T>();

                DontDestroyOnLoad(gameObject); // sets this to not be destroyed when reloading scene
            }
            else
            {
                if (Instance != this)
                {
                    // this enforces our singleton pattern, meaning there can only ever be one instance of a GameManager
                    Destroy(gameObject);
                }
            }

            Initialize();

            Subscribe();
        }
        private void OnDestroy()
        {
            Instance = null;

            UnSubscribe();

            UnInitialize();
        }
    }
}
