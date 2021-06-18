namespace UsefulItems.CSharp.ValidationLib.Interfaces
{
    public interface IProperty<T>
    {
        object Value(T instance);
        string Name { get; }
    }
}
