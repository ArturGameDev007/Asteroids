using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Services.Effects
{
    public interface IEffectResourceManager
    {
        public UniTask LoadEffects();
    }
}