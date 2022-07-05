using System;
using System.Collections.Generic;
using Extensions;
using Helpers;
using UnityEngine;

namespace Managers
{
    public class CalculationManager : BaseManager<CalculationManager>
    {
        public event Action<int> ResultChanged = delegate { };
        public int Result
        {
            get => _result;
            set
            {
                if (_result == value)
                {
                    return;
                }

                if (Debug.isDebugBuild)
                {
                    Debug.Log($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()}" +
                              $"\n{_result}->{value}");
                }
                _result = value;

                ResultChanged.Invoke(_result);
            }
        }
        private int _result;

        private List<int> _values;
        private List<Operand> _operands;

        public override void Initialize()
        {
            _values = new List<int>();
            _operands = new List<Operand>();

            this.InvokeActionAfterAllConditionsAreMet(() =>
                {
                    GameStateManager.Instance.GameState = GameState.CanInputFirstNumber;
                },
                () => GameStateManager.Instance != null &&
                      GameStateManager.Instance.IsInitialized);
        }
        public override void UnInitialize()
        {
        }

        public override void Subscribe()
        {
            this.InvokeActionAfterAllConditionsAreMet(() =>
                {
                    InputManager.Instance.OperandInputted += InputManagerOnOperandInputted;
                },
                () => InputManager.Instance != null &&
                      InputManager.Instance.IsInitialized);
        }
        public override void UnSubscribe()
        {
            if (InputManager.Instance != null)
            {
                InputManager.Instance.OperandInputted -= InputManagerOnOperandInputted;
            }
        }

        public void ConfirmInput()
        {
            if (!InputManager.Instance.InputValue.HasValue)
            {
                return;
            }

            _values.Add(InputManager.Instance.InputValue.Value);
            InputManager.Instance.InputValue = null;

            if (Debug.isDebugBuild)
            {
                Debug.Log($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()}" +
                          $"\n{nameof(_values)}=={_values.ToString<int>()}");
            }

            if (_values.Count == 1)
            {
                GameStateManager.Instance.GameState = GameState.CanInputOperand;
            }
            if (_values.Count == 2)
            {
                GameStateManager.Instance.GameState = GameState.CanGetResult;
            }
        }
        public void Calculate()
        {
            if (_values.Count < 2)
            {
                Debug.LogError($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()} aborted" +
                          $"\nreason - {nameof(_values)}.{nameof(_values.Count)}=={_values.Count}");

                return;
            }
            if (_operands.Count < 1)
            {
                Debug.LogError($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()} aborted" +
                               $"\nreason - {nameof(_operands)}.{nameof(_operands.Count)}=={_operands.Count}");

                return;
            }

            var result = _values[0];
            for (var i = 0; i < _values.Count - 1; i++)
            {
                var value = _values[i + 1];
                var operand = _operands[i];
                switch (operand)
                {
                    case Operand.None:
                        break;
                    case Operand.Plus:
                        result += value;
                        break;
                    case Operand.Minus:
                        result -= value;
                        break;
                    case Operand.Multiply:
                        result *= value;
                        break;
                    case Operand.Divide:
                        result /= value;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            Result = result;

            GameStateManager.Instance.GameState = GameState.CanCancel;
        }
        public void Clear()
        {
            _values.Clear();
            _operands.Clear();

            if (Debug.isDebugBuild)
            {
                Debug.Log($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()}" +
                          $"\n{nameof(_values)}=={_values.ToString<int>()}" +
                          $"\n{nameof(_operands)}=={_operands.ToString<Operand>()}");
            }

            GameStateManager.Instance.GameState = GameState.CanInputFirstNumber;
        }

        private void InputManagerOnOperandInputted(Operand operand)
        {
            _operands.Add(operand);

            if (Debug.isDebugBuild)
            {
                Debug.Log($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()}" +
                          $"\n{nameof(_operands)}=={_operands.ToString<Operand>()}");
            }

            if (_operands.Count == 1)
            {
                GameStateManager.Instance.GameState = GameState.CanInputFirstNumber;
            }
        }
    }
}
