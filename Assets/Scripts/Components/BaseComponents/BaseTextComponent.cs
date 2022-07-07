using TMPro;
using UnityEngine;

namespace Components.BaseComponents
{
    [RequireComponent(typeof(TMP_Text))]
    public class BaseTextComponent : BaseComponent
    {
        [SerializeField]
        private string _replaceblePattern = "<#>";

        protected TMP_Text _text;
        protected string _initialText;

        public override void Initialize()
        {
            _text = this.gameObject.GetComponent<TMP_Text>();
            _initialText = _text.text;
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

        protected void Redraw(string value)
        {
            string newText;

            if (_initialText.Contains(_replaceblePattern))
            {
                newText = _initialText.Replace(_replaceblePattern, value);
            }
            else
            {
                newText = value;
            }

            _text.text = newText;
        }
    }
}
