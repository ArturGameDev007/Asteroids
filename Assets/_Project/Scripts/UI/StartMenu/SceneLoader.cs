using _Project.Scripts.Services.AsyncLoader;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

namespace _Project.Scripts.UI.StartMenu
{
    public class SceneLoader : ISceneLoader
    {
        private const string GAME_SCENE_NAME = "Gameplay";
        private const int LOADING_TIME = 1500;

        private readonly IInstantiator _instantiator;
        private readonly IResourceLoader _resourceLoader;
        private readonly AssetReference _assetReference;

        private ILoadingView _loadingView;

        public SceneLoader(IInstantiator instantiator, AssetReference assetReference, IResourceLoader resourceLoader)
        {
            _instantiator = instantiator;
            _assetReference = assetReference;
            _resourceLoader = resourceLoader;
        }

        public async UniTask LoadSceneAsync()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Debug.LogError("Связь с облаком отсутсвует. Загрузка игры отменена.");
                return;
            }

            var prefab = await _resourceLoader.LoadAssetAsync<LoadingView>(_assetReference);

            _loadingView = _instantiator.InstantiatePrefabForComponent<LoadingView>(prefab);
            _loadingView.Show();

            var download = Addressables.DownloadDependenciesAsync(GAME_SCENE_NAME).ToUniTask();
            var sceneHandle = Addressables.LoadSceneAsync(GAME_SCENE_NAME, activateOnLoad: false);
            var delay = UniTask.Delay(LOADING_TIME);

            await UniTask.WhenAll(download, delay, sceneHandle.ToUniTask());

            if (sceneHandle.Status == AsyncOperationStatus.Succeeded)
                await sceneHandle.Result.ActivateAsync().ToUniTask();

            _resourceLoader.UnloadAsset(_assetReference);
        }
    }
}