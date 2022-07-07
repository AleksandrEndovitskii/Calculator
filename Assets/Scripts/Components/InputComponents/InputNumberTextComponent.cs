using Common;
using Components.BaseComponents;
using Extensions;
using Models;
using UnityEngine;
using Views;

namespace Components.InputComponents
{
    public class InputNumberTextComponent : BaseTextComponent
    {
        [SerializeField]
        private InputNumberView _view;

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
            var inputNumberModel = (InputNumberModel)model;

            var value = inputNumberModel?.Data.ToString();
            Redraw(value);
        }
    }
}
