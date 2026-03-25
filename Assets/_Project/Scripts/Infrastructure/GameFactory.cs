using _Project.Scripts.Services.AsyncLoader;
using _Project.Scripts.UI.GameScreen;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using Zenject;

namespace _Project.Scripts.Infrastructure
{
    public class GameFactory : IGameFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly IResourceLoader _resourceLoader;
        private readonly ILoseModel _loseModel;
        private readonly AssetReference _assetReference;
        
        public GameFactory(IInstantiator instantiator, IResourceLoader resourceLoader, AssetReference assetReference, ILoseModel loseModel)
        {
            _instantiator = instantiator;
            _resourceLoader = resourceLoader;
            _assetReference = assetReference;
            _loseModel = loseModel;
        }

        public async UniTask<LosePresenter> CreateLoseScreenAsync()
        {
            var prefab = await _resourceLoader.LoadAssetAsync<LoseView>(_assetReference);
            
            LoseView loseView = _instantiator.InstantiatePrefabForComponent<LoseView>(prefab);
            
            var presenter= new LosePresenter(loseView, _loseModel);
            presenter.Initialize();

            return presenter;
        }
    }
}