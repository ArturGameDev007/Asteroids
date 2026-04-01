using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Project.Scripts.Services.AsyncLoader
{
    public class AddressableResourceLoader : IResourceLoader
    {
        public async UniTask<T> LoadAssetAsync<T>(AssetReference assetID)
        {
            if (typeof(Component).IsAssignableFrom(typeof(T)))
            {
                var handle = assetID.LoadAssetAsync<GameObject>();
                GameObject prefab = await handle.Task;

                if (prefab != null && prefab.TryGetComponent(out T component))
                    return component;

                throw new NullReferenceException($"Prefab for {typeof(T)} not found");
            }

            var genericHandle = assetID.LoadAssetAsync<T>();
            var asset = await genericHandle;
            
            return asset;
        }

        public void UnloadAsset(AssetReference assetID)
        {
            // assetID.ReleaseAsset();
            if (assetID != null && assetID.Asset != null)
            {
                assetID.ReleaseAsset();
            }
        }
    }
}