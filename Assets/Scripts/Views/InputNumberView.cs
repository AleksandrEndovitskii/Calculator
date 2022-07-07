using Components.BaseComponents;
using Models;
using UnityEngine;

namespace Views
{
    public class InputNumberView : ViewBase<InputNumberModel>
    {
        [SerializeField]
        private int _value;

        public override void Initialize()
        {
            this.Model = new InputNumberModel(_value);
        }
    }
}
