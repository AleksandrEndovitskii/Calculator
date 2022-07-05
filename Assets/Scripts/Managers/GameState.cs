namespace Managers
{
    public enum GameState
    {
        None,
        CanInputNumber,
        CanConfirmNumber,
        CanInputOperand,
        CanConfirmOperand,
        CanGetResult,
        CanClear,
    }
}