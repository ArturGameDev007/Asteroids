using _Project.Scripts.Services.AsyncLoader;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using Zenject;

namespace _Project.Scripts.Player
{
    public class PlayerFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly IResourceLoader _resourceLoader;
        private readonly AssetReference _assetReference;

        public PlayerController Player { get; private set; }

        public PlayerFactory(IInstantiator instantiator, IResourceLoader resourceLoader, [Inject(Id = "ShipRef")] AssetReference assetReference)
        {
            _instantiator = instantiator;
            _resourceLoader = resourceLoader;
            _assetReference = assetReference;
        }

        public async UniTask LoadAsync()
        {
            var prefab = await _resourceLoader.LoadAssetAsync<PlayerController>(_assetReference);

            Player = _instantiator.InstantiatePrefabForComponent<PlayerController>(prefab);
        }

        public void Unload()
        {
            _resourceLoader.UnloadAsset(_assetReference);
        }
    }
}