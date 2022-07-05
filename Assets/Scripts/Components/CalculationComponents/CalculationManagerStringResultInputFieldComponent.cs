using Components.BaseComponents;
using Extensions;
using Managers;

namespace Components.CalculationComponents
{
    public class CalculationManagerStringResultInputFieldComponent : BaseInputFieldComponent
    {
        public override void Subscribe()
        {
            this.InvokeActionAfterAllConditionsAreMet(() =>
                {
                    CalculationManager.Instance.StringResultChanged += CalculationManagerOnStringResultChanged;
                    CalculationManagerOnStringResultChanged(CalculationManager.Instance.StringResult);
                },
                () => CalculationManager.Instance != null &&
                      CalculationManager.Instance.IsInitialized);
        }
        public override void UnSubscribe()
        {
            if (CalculationManager.Instance != null)
            {
                CalculationManager.Instance.StringResultChanged -= CalculationManagerOnStringResultChanged;
            }
        }

        private void Redraw(string text)
        {
            _inputField.text = text;
        }

        private void CalculationManagerOnStringResultChanged(string stringResult)
        {
            Redraw(stringResult);
        }
    }
}
