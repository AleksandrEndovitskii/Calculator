using UnityEngine;
using UnityEngine.UI;

namespace Components.BaseComponents
{
    [RequireComponent(typeof(Button))]
    public class BaseButtonComponent : BaseComponent
    {
        protected Button _button;

        public override void Initialize()
        {
            _button = this.gameObject.GetComponent<Button>();
        }
        public override void UnInitialize()
        {
        }

        public override void Subscribe()
        {
            _button.onClick.AddListener(ButtonOnClick);
        }
        public override void UnSubscribe()
        {
            _button.onClick.RemoveListener(ButtonOnClick);
        }

        protected virtual void ButtonOnClick()
        {

        }
    }
}
