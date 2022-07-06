using System;
using Managers;

namespace Helpers
{
    public static class OperandHelper
    {
        public static string ToString(Operand operand)
        {
            var result = string.Empty;
            switch (operand)
            {
                case Operand.None:
                    result = "";
                    break;
                case Operand.Plus:
                    result = "+";
                    break;
                case Operand.Minus:
                    result = "-";
                    break;
                case Operand.Multiply:
                    result = "*";
                    break;
                case Operand.Divide:
                    result = "/";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(operand), operand, null);
            }
            return result;
        }
    }
}
