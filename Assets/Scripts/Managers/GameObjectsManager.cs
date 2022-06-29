using Extensions;
using UnityEngine;
using Views;

namespace Managers
{
    public class GameObjectsManager : BaseManager<GameObjectsManager>
    {
        [SerializeReference]
        private CalculatorView _viewPrefab;
        private CalculatorView _viewInstance;

        public override void Initialize()
        {
            this.InvokeActionAfterAllConditionsAreMet(() =>
                {
                    _viewInstance = Instantiate(_viewPrefab, UserInterfaceManager.Instance.UserInterfaceCanvasInstance.transform);
                },
                () => UserInterfaceManager.Instance != null &&
                      UserInterfaceManager.Instance.IsInitialized);

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
