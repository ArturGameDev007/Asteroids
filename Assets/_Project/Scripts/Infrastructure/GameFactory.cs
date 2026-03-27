using _Project.Scripts.Player;
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
        private readonly AssetReference _loseScreenReference;
        
        // private readonly PlayerFactory _playerFactory;
        // private readonly AssetReference _playerReference;
        
        public GameFactory(IInstantiator instantiator, IResourceLoader resourceLoader, AssetReference loseScreenReference, ILoseModel loseModel)
        {
            _instantiator = instantiator;
            _resourceLoader = resourceLoader;
            _loseScreenReference = loseScreenReference;
            _loseModel = loseModel;
        }

        // public async UniTask<PlayerController> CreatePlayerAsync()
        // {
        //     // var asset = await _playerFactory.LoadAsync();
        //     
        //     PlayerController player = _instantiator.InstantiatePrefabForComponent<PlayerController>(asset);
        //
        //     return player;
        // }

        public async UniTask<LosePresenter> CreateLoseScreenAsync()
        {
            var prefab = await _resourceLoader.LoadAssetAsync<LoseView>(_loseScreenReference);
            
            LoseView loseView = _instantiator.InstantiatePrefabForComponent<LoseView>(prefab);
            
            var presenter= new LosePresenter(loseView, _loseModel);
            presenter.Initialize();

            return presenter;
        }
    }
}