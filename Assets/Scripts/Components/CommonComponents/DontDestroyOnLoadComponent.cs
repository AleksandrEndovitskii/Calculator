using Components.BaseComponents;

namespace Components.CommonComponents
{
    public class DontDestroyOnLoadComponent : BaseComponent
    {
        public override void Initialize()
        {
            DontDestroyOnLoad(gameObject);
        }
        public override void UnInitialize()
        {
        }

        public override void Subscribe()
        {
        }
        public override void UnSubscribe()
        {
        }
    }
}
