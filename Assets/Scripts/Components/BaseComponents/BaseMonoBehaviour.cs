using UnityEngine;

namespace Components.BaseComponents
{
    public abstract class BaseMonoBehaviour : MonoBehaviour
    {
        public void ReInitialize()
        {
            UnInitialize();
            Initialize();
        }

        public abstract void Initialize();
        public abstract void UnInitialize();
        public abstract void Subscribe();
        public abstract void UnSubscribe();
    }
}
