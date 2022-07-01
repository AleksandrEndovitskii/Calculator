using Components.BaseComponents;
using Extensions;
using Managers;
using TMPro;
using UnityEngine;

namespace Components.InputFieldComponents
{
    [RequireComponent(typeof(TMP_InputField))]
    public class InputFieldManagerInputFieldRegistrationComponent : BaseComponent
    {
        private TMP_InputField _inputField;

        public override void Initialize()
        {
            _inputField = this.gameObject.GetComponent<TMP_InputField>();

            this.InvokeActionAfterAllConditionsAreMet(() =>
                {
                    InputFieldManager.Instance.Register(_inputField);
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
        }
    }
}
