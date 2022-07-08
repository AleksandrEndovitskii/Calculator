using Components.BaseComponents;
using Managers;
using Models;
using UnityEngine;

namespace Views
{
    public class InputOperandView : ViewBase<InputOperandModel>
    {
        [SerializeField]
        private Operand _operand;

        public override void Initialize()
        {
            this.Model = new InputOperandModel(_operand);
        }
    }
}
