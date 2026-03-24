using _Project.Scripts.Services.AsyncLoader;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace _Project.Scripts.UI.StartMenu
{
    public class SceneLoader : ISceneLoader
    {
        private const string GAME_SCENE_NAME = "Gameplay";
        private const int LOADING_TIME = 1500;

        private readonly IResourceLoader _resourceLoader;
        private readonly AssetReference _assetReference;
        
        private ILoadingView _loadingView;

        public SceneLoader(AssetReference assetReference, IResourceLoader resourceLoader, ILoadingView  loadingView)
        {
            _assetReference = assetReference;
            _resourceLoader = resourceLoader;
            _loadingView = loadingView;
        }

        public async UniTask LoadSceneAsync()
        {
            _loadingView = await _resourceLoader.LoadAsset<LoadingView>(_assetReference);
            _loadingView.Show();

            await UniTask.WhenAll(UniTask.Delay(LOADING_TIME), Addressables.LoadSceneAsync(GAME_SCENE_NAME).ToUniTask());
        }
    }
}