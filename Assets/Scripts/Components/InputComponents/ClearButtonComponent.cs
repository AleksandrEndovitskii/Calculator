using Components.BaseComponents;
using Managers;

namespace Components.InputComponents
{
    public class ClearButtonComponent : BaseButtonComponent
    {
        protected override void ButtonOnClick()
        {
            CalculationManager.Instance.Clear();
        }
    }
}
