using _Project.Scripts.Services.AsyncLoader;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using Zenject;

namespace _Project.Scripts.Player
{
    public class PlayerResourceManager
    {
        private readonly IInstantiator _instantiator;
        private readonly IPlayerProvider _playerProvider;

        private readonly PlayerController _player;
        private readonly IResourceLoader _resourceLoader;
        private readonly AssetReference _playerReference;

        public PlayerResourceManager(IInstantiator instantiator, IResourceLoader resourceLoader,
            AssetReference playerReference, IPlayerProvider playerProvider)
        {
            _instantiator = instantiator;
            _resourceLoader = resourceLoader;
            _playerReference = playerReference;
            _playerProvider = playerProvider;
        }

        public async UniTask<PlayerController> LoadAsync()
        {
            var prefab = await _resourceLoader.LoadAssetAsync<PlayerController>(_playerReference);
            var asset = _instantiator.InstantiatePrefabForComponent<PlayerController>(prefab);
     
            _playerProvider.Setup(asset);
        
            return asset;
        }

        public void Unload()
        {
            _resourceLoader.UnloadAsset(_playerReference);
        }
    }
}