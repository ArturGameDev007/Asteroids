using _Project.Scripts.Services.AsyncLoader;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Project.Scripts.UI.StartMenu
{
    public class SceneLoader : ISceneLoader
    {
        private const string GAME_SCENE_NAME = "Gameplay";
        private const int LOADING_TIME = 1500;

        private readonly IResourceLoader _resourceLoader;
        private readonly AssetReference _assetReference;
        // private readonly ILoadingView _loadingView;

        public SceneLoader(AssetReference assetReference, IResourceLoader resourceLoader)
        {
            // _loadingView = loadingView;
            _assetReference = assetReference;
            _resourceLoader = resourceLoader;
        }

        // public void LoadScene()
        // {
        //     SceneManager.LoadScene(GAME_SCENE_NAME);
        // }

        public async UniTask LoadSceneAsync()
        {
            // var prefab = await _resourceLoader.LoadAsset<GameObject>(_assetReference);
            // var loadingView=Object.Instantiate(prefab);

            var loadingView = await _resourceLoader.LoadAsset<LoadingView>(_assetReference);

            loadingView.Show();

            // _loadingView.Show();

            await UniTask.Delay(LOADING_TIME);
            await Addressables.LoadSceneAsync(GAME_SCENE_NAME).ToUniTask();
        }
    }
}