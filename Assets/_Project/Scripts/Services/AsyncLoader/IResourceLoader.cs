using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Project.Scripts.Services.AsyncLoader
{
    public interface IResourceLoader
    {
        public UniTask<T> LoadAssetAsync<T>(AssetReference assetID) where T: Object;

        public void UnloadAsset(AssetReference assetID);
    }
}