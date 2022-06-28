using UnityEngine;
using UnityEngine.EventSystems;

namespace Managers
{
    public class UserInterfaceManager : BaseManager<UserInterfaceManager>
    {
        [SerializeField]
        private Canvas _userInterfaceCanvasPrefab;
        private Canvas _userInterfaceCanvasInstance;
        [SerializeField]
        private EventSystem _userInterfaceEventSystemPrefab;
        private EventSystem _userInterfaceEventSystemInstance;

        public override void Initialize()
        {
            _userInterfaceCanvasInstance = Instantiate(_userInterfaceCanvasPrefab);
            _userInterfaceEventSystemInstance = Instantiate(_userInterfaceEventSystemPrefab);

            IsInitialized = true;
        }
        public override void UnInitialize()
        {
        }

        public override void Subscribe()
        {
        }
        public override void UnSubscribe()
        {
        }
    }
}
