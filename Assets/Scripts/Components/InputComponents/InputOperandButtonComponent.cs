using Components.BaseComponents;
using Managers;
using UnityEngine;
using Views;

namespace Components.InputComponents
{
    public class InputOperandButtonComponent : BaseButtonComponent
    {
        [SerializeField]
        private InputOperandView _view;

        protected override void ButtonOnClick()
        {
            if (_view == null)
            {
                return;
            }

            if (_view.Model == null)
            {
                return;
            }

            InputManager.Instance.HandleInput(_view.Model.Data);
        }
    }
}
