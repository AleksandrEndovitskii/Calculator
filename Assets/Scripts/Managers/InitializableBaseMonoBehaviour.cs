using System;
using Components;
using Components.BaseComponents;
using Helpers;
using UnityEngine;

namespace Managers
{
    public abstract class InitializableBaseMonoBehaviour: BaseMonoBehaviour
    {
        public event Action<bool> IsInitializedChanged = delegate { };

        public bool IsInitialized
        {
            get => _isInitialized;
            internal set
            {
                if (_isInitialized == value)
                {
                    return;
                }

                Debug.Log($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()}" +
                          $"\n{_isInitialized}->{value}");
                _isInitialized = value;

                IsInitializedChanged.Invoke(_isInitialized);
            }
        }

        private bool _isInitialized;
    }
}
