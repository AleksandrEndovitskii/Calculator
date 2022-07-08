using Common;
using Components.BaseComponents;
using Extensions;
using Helpers;
using Models;
using UnityEngine;
using Views;

namespace Components.InputComponents
{
    public class InputOperandTextComponent : BaseTextComponent
    {
        [SerializeField]
        private InputOperandView _view;

        public override void Subscribe()
        {
            this.InvokeActionAfterAllConditionsAreMet(() =>
                {
                    _view.ModelChanged += ViewViewModelChanged;
                    ViewViewModelChanged(_view.Model);
                },
                () =>  _view != null);
        }
        public override void UnSubscribe()
        {
            if (_view != null)
            {
                _view.ModelChanged -= ViewViewModelChanged;
            }
        }

        private void ViewViewModelChanged(IModel model)
        {
            var inputOperandModel = (InputOperandModel)model;

            if (inputOperandModel == null)
            {
                return;
            }

            var operandString = OperandHelper.ToString(inputOperandModel.Data);
            Redraw(operandString);
        }
    }
}
