using System;
using Helpers;
using UnityEngine;

namespace Managers
{
    public class InputManager : BaseManager<InputManager>
    {
        public event Action<int?> InputValueChanged = delegate { };
        public int? InputValue
        {
            get => _inputValue;
            set
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
        private int? _inputValue;

        public event Action<Operand> InputOperandChanged = delegate { };
        public Operand InputOperand
        {
            get => _inputOperand;
            set
            {
                if (_inputOperand == value)
                {
                    return;
                }

                if (Debug.isDebugBuild)
                {
                    Debug.Log($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()}" +
                              $"\n{_inputOperand}->{value}");
                }
                _inputOperand = value;

                InputOperandChanged.Invoke(_inputOperand);
            }
        }
        private Operand _inputOperand;

        public override void Initialize()
        {
            InputValue = null;
            InputOperand = Operand.None;

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

        public void HandleInput(int value)
        {
            var stringValue = string.Empty;
            if (InputValue.HasValue)
            {
                stringValue = InputValue.Value.ToString();
            }
            stringValue += value;
            var intValue = int.Parse(stringValue);
            InputValue = intValue;
        }
        public void HandleInput(Operand operand)
        {
            InputOperand = operand;
        }
    }
}
