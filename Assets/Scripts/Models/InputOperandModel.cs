using Common;
using Managers;

namespace Models
{
    public class InputOperandModel : BaseModel<Operand>
    {
        public InputOperandModel(Operand data) : 
            base(data)
        {
        }
    }
}
