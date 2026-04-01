using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace _Project.Scripts.Services.AsyncLoader
{
    public interface IResourceLoader
    {
        public UniTask<T> LoadAssetAsync<T>(AssetReference assetID);

        public void UnloadAsset(AssetReference assetID);
    }
}