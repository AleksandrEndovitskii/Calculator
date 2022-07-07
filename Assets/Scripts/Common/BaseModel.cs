using System;
using Helpers;
using UnityEngine;

namespace Common
{
    public class BaseModel<T> : IModel
    {
        public event Action<T> DataChanged = delegate { };
        public T Data
        {
            get => _data;
            private set
            {
                if (_data.Equals(value))
                {
                    return;
                }

                if (Debug.isDebugBuild)
                {
                    Debug.Log($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()}" +
                              $"\n{_data}->{value}");
                }
                _data = value;

                DataChanged.Invoke(_data);
            }
        }
        private T _data;

        public BaseModel(T data)
        {
            Data = data;
        }
    }
}
