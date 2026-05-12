using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Services.Audio.SFX
{
    public interface IAudioResourceManager
    {
        public UniTask LoadClips();
    }
}