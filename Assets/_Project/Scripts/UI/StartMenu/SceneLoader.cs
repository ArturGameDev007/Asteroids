using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace _Project.Scripts.UI.StartMenu
{
    public class SceneLoader : ISceneLoader
    {
        private const string GAME_SCENE_NAME = "Gameplay";
        private const int LOADING_TIME = 1500;

        private readonly ILoadingView _loadingView;

        public SceneLoader(ILoadingView loadingView)
        {
            _loadingView = loadingView;
        }

        // public void LoadScene()
        // {
        //     SceneManager.LoadScene(GAME_SCENE_NAME);
        // }

        public async UniTask LoadSceneAsync()
        {
            _loadingView.Show();
            
            await UniTask.Delay(LOADING_TIME);
            
            await Addressables.LoadSceneAsync(GAME_SCENE_NAME).ToUniTask();
        }
    }
} 