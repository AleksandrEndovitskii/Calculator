using Components.BaseComponents;
using Managers;

namespace Components.InputComponents
{
    public class CalculateButtonComponent : BaseButtonComponent
    {
        protected override void ButtonOnClick()
        {
            CalculationManager.Instance.Calculate();
        }
    }
}
