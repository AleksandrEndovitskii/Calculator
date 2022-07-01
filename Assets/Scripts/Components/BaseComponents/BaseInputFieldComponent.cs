using System;
using TMPro;
using UnityEngine;

namespace Components.BaseComponents
{
    [RequireComponent(typeof(TMP_InputField))]
    public class BaseInputFieldComponent : BaseComponent
    {
        public event Action<string> ValueChanged = delegate { };
        public event Action<string> EndEdit = delegate { };

        protected TMP_InputField _inputField;

        public override void Initialize()
        {
            _inputField = this.gameObject.GetComponent<TMP_InputField>();
        }
        public override void UnInitialize()
        {
        }

        public override void Subscribe()
        {
            _inputField.onValueChanged.AddListener(InputFieldOnValueChanged);
            _inputField.onEndEdit.AddListener(InputFieldOnEndEdit);
        }
        public override void UnSubscribe()
        {
            _inputField.onValueChanged.RemoveListener(InputFieldOnValueChanged);
            _inputField.onEndEdit.RemoveListener(InputFieldOnEndEdit);
        }

        private void InputFieldOnValueChanged(string value)
        {
            ValueChanged.Invoke(value);
        }
        private void InputFieldOnEndEdit(string value)
        {
            EndEdit.Invoke(value);
        }
    }
}
