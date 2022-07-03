using Components.BaseComponents;
using Managers;
using UnityEngine;

namespace Components.InputComponents
{
    public class InputButtonComponent : BaseButtonComponent
    {
        [SerializeField]
        private string _value;

        protected override void ButtonOnClick()
        {
            InputManager.Instance.HandleInput(_value);
        }
    }
}
