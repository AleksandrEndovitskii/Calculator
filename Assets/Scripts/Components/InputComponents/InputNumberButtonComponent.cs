using Components.BaseComponents;
using Managers;
using UnityEngine;

namespace Components.InputComponents
{
    public class InputNumberButtonComponent : BaseButtonComponent
    {
        [SerializeField]
        private int _value;

        protected override void ButtonOnClick()
        {
            InputManager.Instance.HandleInput(_value);
        }
    }
}
