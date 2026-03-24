using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Project.Scripts.Services.AsyncLoader
{
    public interface IResourceLoader
    {
        // public UniTask<T> LoadAsset<T>(AssetReference assetReference) where T: Object;
        
        public UniTask<T> LoadAsset<T>(AssetReference assetReference) where T: Object;
        
        public void UnloadAsset();
        
        // public UniTask InstantiateAsync<T>(AssetReference assetReference, Transform parent) where T: Object;
    }
}