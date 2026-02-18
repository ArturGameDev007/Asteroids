namespace _Project.Scripts.Enemies
{
    public interface IObjectReturner<T>
    {
        public void ReturnPool(T objectType);
    }
}