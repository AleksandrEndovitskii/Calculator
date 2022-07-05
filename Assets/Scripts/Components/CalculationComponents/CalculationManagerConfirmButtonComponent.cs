using Components.BaseComponents;
using Managers;

namespace Components.CalculationComponents
{
    public class CalculationManagerConfirmButtonComponent : BaseButtonComponent
    {
        protected override void ButtonOnClick()
        {
            CalculationManager.Instance.ConfirmInput();
        }
    }
}
