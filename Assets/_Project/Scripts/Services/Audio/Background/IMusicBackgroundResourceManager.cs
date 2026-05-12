using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Services.Audio.Background
{
    public interface IMusicBackgroundResourceManager
    {
        public UniTask LoadMusic();
        
        public void PlayMusic();
        
        public void StopMusic();
    }
}