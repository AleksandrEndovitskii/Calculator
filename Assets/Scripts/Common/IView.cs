namespace Common
{
    public interface IView<T> where T : IModel
    {
        public T Model { get; set; }
    }
}
