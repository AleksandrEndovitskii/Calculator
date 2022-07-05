using Components.BaseComponents;
using Managers;

namespace Components.CalculationComponents
{
    public class CalculationManagerCalculateButtonComponent : BaseButtonComponent
    {
        protected override void ButtonOnClick()
        {
            CalculationManager.Instance.Calculate();
        }
    }
}
