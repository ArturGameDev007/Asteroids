using _Project.Scripts.Services.AsyncLoader;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using Zenject;

namespace _Project.Scripts.Player
{
    public class PlayerResourceManager
    {
        private readonly IInstantiator _instantiator;
        private readonly IResourceLoader _resourceLoader;
        private readonly AssetReference _playerReference;

        public PlayerResourceManager(IInstantiator instantiator, IResourceLoader resourceLoader, AssetReference playerReference)
        {
            _instantiator = instantiator;
            _resourceLoader = resourceLoader;
            _playerReference = playerReference;
        }

        public async UniTask<PlayerController> LoadAsync()
        {
            var prefab = await _resourceLoader.LoadAssetAsync<PlayerController>(_playerReference);

            PlayerController player = _instantiator.InstantiatePrefabForComponent<PlayerController>(prefab);

            return player;
        }

        public void Unload()
        {
            _resourceLoader.UnloadAsset(_playerReference);
        }
    }
}