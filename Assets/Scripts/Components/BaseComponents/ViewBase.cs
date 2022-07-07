using System;
using Common;
using Helpers;
using UnityEngine;

namespace Components.BaseComponents
{
    public class ViewBase<T> : BaseComponent, IView<T> where T : IModel
    {
        public Action<T> ModelChanged = delegate { };
        public T Model
        {
            get
            {
                return _model;
            }
            set
            {
                if (Equals(value, _model))
                {
                    return;
                }

                if (Debug.isDebugBuild)
                {
                    Debug.Log($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()}" +
                              $"\n{_model}->{value}");
                }
                _model = value;

                ModelChanged.Invoke(_model);
            }
        }
        private T _model;

        public override void Initialize()
        {
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
