using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Infrastructure
{
    public interface IGameLoader
    {
        public UniTask LoadAllAsync();
        
        public void UnloadAll();
    }
}