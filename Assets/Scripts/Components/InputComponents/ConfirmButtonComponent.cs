using Components.BaseComponents;
using Managers;

namespace Components.InputComponents
{
    public class ConfirmButtonComponent : BaseButtonComponent
    {
        protected override void ButtonOnClick()
        {
            CalculationManager.Instance.ConfirmInput();
        }
    }
}
