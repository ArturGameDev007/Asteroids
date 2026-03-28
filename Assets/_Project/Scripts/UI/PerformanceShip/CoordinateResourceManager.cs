using _Project.Scripts.Services.AsyncLoader;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using Zenject;

namespace _Project.Scripts.UI.PerformanceShip
{
    public class CoordinateResourceManager
    {
        private readonly IInstantiator _instantiator;
        private readonly IResourceLoader _resourceLoader;
        private readonly AssetReference _assetReference;
        
        public ICoordinateView View { get; private set; }

        public CoordinateResourceManager(IInstantiator instantiator, IResourceLoader resourceLoader,
            AssetReference assetReference)
        {
            _instantiator = instantiator;
            _resourceLoader = resourceLoader;
            _assetReference = assetReference;
        }

        public async UniTask LoadAsyncPerformanceShip()
        {
            var asset = await _resourceLoader.LoadAssetAsync<CoordinateDisplay>(_assetReference);

            View = _instantiator.InstantiatePrefabForComponent<CoordinateDisplay>(asset);
        }

        public void Unload()
        {
            _resourceLoader.UnloadAsset(_assetReference);
        }
    }
}