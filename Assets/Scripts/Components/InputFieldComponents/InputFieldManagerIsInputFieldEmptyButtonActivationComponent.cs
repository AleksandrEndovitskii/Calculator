using Components.BaseComponents;
using Extensions;
using Managers;
using UnityEngine;

namespace Components.InputFieldComponents
{
    public class InputFieldManagerIsInputFieldEmptyButtonActivationComponent : BaseButtonComponent
    {
        [SerializeField]
        private bool _isReverted;

        public override void Initialize()
        {
            base.Initialize();

            this.InvokeActionAfterAllConditionsAreMet(() =>
                {
                    InputFieldManager.Instance.IsInputFieldEmptyChanged += InputFieldManagerOnIsInputFieldEmptyChanged;
                    InputFieldManagerOnIsInputFieldEmptyChanged(InputFieldManager.Instance.IsInputFieldEmpty);
                },
                () => InputFieldManager.Instance != null &&
                      InputFieldManager.Instance.IsInitialized);
        }
        public override void UnInitialize()
        {
        }

        public override void Subscribe()
        {
        }
        public override void UnSubscribe()
        {
            if (InputFieldManager.Instance != null)
            {
                InputFieldManager.Instance.IsInputFieldEmptyChanged -= InputFieldManagerOnIsInputFieldEmptyChanged;
            }
        }

        private void Redraw(bool isActive)
        {
            _button.interactable = isActive;
        }

        private void InputFieldManagerOnIsInputFieldEmptyChanged(bool isInputFieldEmpty)
        {
            var isActive = InputFieldManager.Instance.IsHaveInputField && 
                           isInputFieldEmpty;
            if (_isReverted)
            {
                isActive = !isActive;
            }
            Redraw(isActive);
        }
    }
}
