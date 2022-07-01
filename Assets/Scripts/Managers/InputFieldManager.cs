using System;
using Helpers;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class InputFieldManager : BaseManager<InputFieldManager>
    {
        public event Action<bool> IsInputFieldEmptyChanged = delegate { };
        public bool IsInputFieldEmpty
        {
            get => _isInputFieldEmpty;
            internal set
            {
                if (_isInputFieldEmpty == value)
                {
                    return;
                }

                if (Debug.isDebugBuild)
                {
                    Debug.Log($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()}" +
                              $"\n{_isInputFieldEmpty}->{value}");
                }
                _isInputFieldEmpty = value;

                IsInputFieldEmptyChanged.Invoke(_isInputFieldEmpty);
            }
        }
        private bool _isInputFieldEmpty;

        public bool IsHaveInputField
        {
            get
            {
                var result = false;

                result = _inputField != null;

                return result;
            }
        }

        private TMP_InputField _inputField;

        public override void Initialize()
        {
            // input field is empty from the start
            _isInputFieldEmpty = true;

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

        public void Register(TMP_InputField inputField)
        {
            _inputField = inputField;
            _inputField.onValueChanged.AddListener(InputFieldOnValueChanged);
        }
        public void UnRegister(TMP_InputField inputField)
        {
            if (_inputField != inputField)
            {
                return;
            }

            _inputField.onValueChanged.RemoveListener(InputFieldOnValueChanged);
            _inputField = null;
        }

        private void InputFieldOnValueChanged(string value)
        {
            if (Debug.isDebugBuild)
            {
                Debug.Log($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()}" +
                          $"\n{nameof(value)}->{value}");
            }

            IsInputFieldEmpty = string.IsNullOrEmpty(value);
        }
    }
}
