using Components.BaseComponents;
using Managers;

namespace Components.CalculationComponents
{
    public class CalculationManagerClearButtonComponent : BaseButtonComponent
    {
        protected override void ButtonOnClick()
        {
            CalculationManager.Instance.Clear();
        }
    }
}
