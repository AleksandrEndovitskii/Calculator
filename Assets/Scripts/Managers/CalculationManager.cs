using System;
using System.Collections.Generic;
using Extensions;
using Helpers;
using UnityEngine;

namespace Managers
{
    public class CalculationManager : BaseManager<CalculationManager>
    {
         // uniRX
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
        public event Action<string> StringResultChanged = delegate { };
        public string StringResult
        {
            get => _stringResult;
            set
            {
                if (_stringResult == value)
                {
                    return;
                }

                if (Debug.isDebugBuild)
                {
                    Debug.Log($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()}" +
                              $"\n{_stringResult}->{value}");
                }
                _stringResult = value;

                StringResultChanged.Invoke(_stringResult);
            }
        }
        private string _stringResult;

        private List<int> _values;
        private List<Operand> _operands;

        public override void Initialize()
        {
            _result = 0;
            _stringResult = string.Empty;
            _values = new List<int>();
            _operands = new List<Operand>();

            this.InvokeActionAfterAllConditionsAreMet(() =>
                {
                    GameStateManager.Instance.GameState = GameState.CanInputNumber;

                    Instance.IsInitialized = true;
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
                    InputManager.Instance.InputValueChanged += InputManagerOnInputValueChanged;
                    InputManagerOnInputValueChanged(InputManager.Instance.InputValue);
                },
                () => InputManager.Instance != null &&
                      InputManager.Instance.IsInitialized);
            this.InvokeActionAfterAllConditionsAreMet(() =>
                {
                    InputManager.Instance.InputOperandChanged += InputManagerOnInputOperandChanged;
                    InputManagerOnInputOperandChanged(InputManager.Instance.InputOperand);
                },
                () => InputManager.Instance != null &&
                      InputManager.Instance.IsInitialized);
        }
        public override void UnSubscribe()
        {
            if (InputManager.Instance != null)
            {
                InputManager.Instance.InputValueChanged -= InputManagerOnInputValueChanged;
            }
            if (InputManager.Instance != null)
            {
                InputManager.Instance.InputOperandChanged -= InputManagerOnInputOperandChanged;
            }
        }

        public void ConfirmInput()
        {
            if (InputManager.Instance.InputValue.HasValue)
            {
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
            if (InputManager.Instance.InputOperand != Operand.None)
            {
                _operands.Add(InputManager.Instance.InputOperand);
                InputManager.Instance.InputOperand = Operand.None;

                if (Debug.isDebugBuild)
                {
                    Debug.Log($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()}" +
                              $"\n{nameof(_operands)}=={_operands.ToString<Operand>()}");
                }

                if (_operands.Count == 1)
                {
                    GameStateManager.Instance.GameState = GameState.CanInputNumber;
                }
            }

            var stringResult = GetStringResult();
            StringResult = stringResult;
        }
        public void Calculate()
        {
            if (_values.Count < 2)
            {
                if (Debug.isDebugBuild)
                {
                    Debug.LogError($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()} aborted" +
                                $"\nreason - {nameof(_values)}.{nameof(_values.Count)}=={_values.Count}");
                }

                return;
            }
            if (_operands.Count < 1)
            {
                if (Debug.isDebugBuild)
                {
                    Debug.LogError($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()} aborted" +
                                $"\nreason - {nameof(_operands)}.{nameof(_operands.Count)}=={_operands.Count}");
                }

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
            var stringResult = GetStringResult();
            StringResult = stringResult + "=" + Result;

            GameStateManager.Instance.GameState = GameState.CanClear;
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

            Result = 0;
            StringResult = string.Empty;

            GameStateManager.Instance.GameState = GameState.CanInputNumber;
        }

        private string GetStringResult()
        {
            var stringResult = string.Empty;
            var operandsIndex = 0;
            for (var valuesIndex = 0; valuesIndex < _values.Count; valuesIndex++)
            {
                var value = _values[valuesIndex];
                stringResult += value;

                if (operandsIndex < _operands.Count)
                {
                    var operand = _operands[operandsIndex];
                    operandsIndex++;
                    stringResult += OperandHelper.ToString(operand);
                }
            }

            return stringResult;
        }

        private void InputManagerOnInputValueChanged(int? value)
        {
            if (!value.HasValue)
            {
                if (Debug.isDebugBuild)
                {
                    Debug.LogWarning($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()} aborted" +
                                  $"\nreason - {nameof(value)}." +
                                  $"{nameof(value.HasValue)}=={value.HasValue}");
                }

                return;
            }

            GameStateManager.Instance.GameState = GameState.CanConfirmNumber;
        }
        private void InputManagerOnInputOperandChanged(Operand operand)
        {
            if (operand == Operand.None)
            {
                if (Debug.isDebugBuild)
                {
                    Debug.LogWarning($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()} aborted" +
                                  $"\n{nameof(operand)}=={operand}");
                }

                return;
            }

            GameStateManager.Instance.GameState = GameState.CanConfirmOperand;
        }
    }
}
