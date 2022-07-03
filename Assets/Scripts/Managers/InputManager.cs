using System;
using Helpers;
using UnityEngine;

namespace Managers
{
    public class InputManager : BaseManager<InputManager>
    {
        public event Action<string> InputValueChanged = delegate { };
        public string InputValue
        {
            get => _inputValue;
            private set
            {
                if (_inputValue == value)
                {
                    return;
                }

                if (Debug.isDebugBuild)
                {
                    Debug.Log($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()}" +
                              $"\n{_inputValue}->{value}");
                }
                _inputValue = value;

                InputValueChanged.Invoke(_inputValue);
            }
        }
        private string _inputValue;

        public override void Initialize()
        {
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

        public void HandleInput(string value)
        {
            InputValue += value;
        }
    }
}
