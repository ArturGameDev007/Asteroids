using _Project.Scripts.Services.AsyncLoader;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace _Project.Scripts.UI.BackToStartMenu
{
    public class PanelBackToMenuResourceManager : IBackToMenuManager
    {
        private readonly IResourceLoader _resourceLoader;
        private readonly AssetReference _assetReference;

        public PanelBackToMenuResourceManager(IResourceLoader resourceLoader,
            AssetReference assetReference)
        {
            _resourceLoader = resourceLoader;
            _assetReference = assetReference;
        }

        public async UniTask LoadBackButton()
        {
            await _resourceLoader.LoadAssetAsync<GameplayView>(_assetReference);
        }
    }
}