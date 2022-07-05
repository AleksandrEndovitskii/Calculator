using System.Collections.Generic;
using Helpers;
using UnityEngine;

namespace Managers
{
    public class CalculationManager : BaseManager<CalculationManager>
    {
        private List<string> _values;

        public override void Initialize()
        {
            _values = new List<string>();
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

        public void ConfirmInput()
        {
            _values.Add(InputManager.Instance.InputValue);
            InputManager.Instance.InputValue = string.Empty;

            if (Debug.isDebugBuild)
            {
                Debug.Log($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()}" +
                          $"\n{nameof(_values)}=={_values.ToString<string>()}");
            }
        }
    }
}
