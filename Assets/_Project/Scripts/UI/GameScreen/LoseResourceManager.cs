using _Project.Scripts.Services.AsyncLoader;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace _Project.Scripts.UI.GameScreen
{
    public class LoseResourceManager
    {
        private readonly IResourceLoader _resourceLoader;
        private readonly AssetReference _loseReference;
        
        private LoseView _loseViewAsset;

        public LoseResourceManager(IResourceLoader resourceLoader, AssetReference loseReference)
        {
            _resourceLoader = resourceLoader;
            _loseReference = loseReference;
        }

        public async UniTask<LoseView> LoseScreenLoadAsync()
        {
            _loseViewAsset = await _resourceLoader.LoadAssetAsync<LoseView>(_loseReference);
            
            return _loseViewAsset;
        }

        public void Unload()
        {
            _resourceLoader.UnloadAsset(_loseReference);
        }
    }
}