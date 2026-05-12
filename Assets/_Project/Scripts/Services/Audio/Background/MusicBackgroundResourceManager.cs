using _Project.Scripts.Services.AsyncLoader;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using Zenject;

namespace _Project.Scripts.Services.Audio.Background
{
    public class MusicBackgroundResourceManager : IMusicBackgroundResourceManager
    {
        private readonly AssetReference _backgroundClip;
        private readonly IResourceLoader _resourceLoader;
        private readonly IInstantiator _instantiator;
        
        public MusicBackground Music { get; private set; }

        public MusicBackgroundResourceManager(AssetReference backgroundClip, IResourceLoader resourceLoader,  IInstantiator instantiator)
        {
            _backgroundClip = backgroundClip;
            _resourceLoader = resourceLoader;
            _instantiator = instantiator;
        }

        public async UniTask LoadMusic()
        {
            var prefab = await _resourceLoader.LoadAssetAsync<MusicBackground>(_backgroundClip);

            if (prefab != null)
                Music = _instantiator.InstantiatePrefabForComponent<MusicBackground>(prefab);
        }

        public void PlayMusic()
        {
            if (Music != null)
                Music.PlayBackgroundMusic();
        }

        public void StopMusic()
        {
            if (Music != null)
                Music.StopBackgroundMusic();
        }
    }
}