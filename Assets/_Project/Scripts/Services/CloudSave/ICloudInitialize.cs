using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Infrastructure
{
    public interface ICloudInitialize
    {
        public UniTask InitializeCloud();
    }
}