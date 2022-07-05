using Components.BaseComponents;
using Managers;
using UnityEngine;

namespace Components.InputComponents
{
    public class InputOperandButtonComponent : BaseButtonComponent
    {
        [SerializeField]
        private Operand _operand;

        protected override void ButtonOnClick()
        {
            InputManager.Instance.HandleInput(_operand);
        }
    }
}
