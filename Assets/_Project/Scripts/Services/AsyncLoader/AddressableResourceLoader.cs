using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace _Project.Scripts.Services.AsyncLoader
{
    public class AddressableResourceLoader : IResourceLoader
    {
        // public UniTask<T> LoadResourceAsync<T>(string adress) where T : Object
        // {
        //     throw new System.NotImplementedException();
        // }
        //
        // public void Unload<T>(T asset) where T : Object
        // {
        //     Addressables.Release(asset);
        // }
        
        private GameObject _cachedObject;

        public async UniTask<T> LoadAsset<T>(AssetReference assetReference) where T : Object
        {
            // var asset = await Addressables.LoadAssetAsync<T>(assetReference);
            // return asset;
            
            var handle=Addressables.InstantiateAsync(assetReference);
            _cachedObject = await handle.Task;

            if (_cachedObject.TryGetComponent(out T component) ==  false)
            {
                throw new NullReferenceException($"Object of type {typeof(T)}");
            }
            
            return component;
        }

        public void UnloadAsset()
        {
            if (_cachedObject == null)
                return;
            
            _cachedObject.SetActive(false);
            Addressables.ReleaseInstance(_cachedObject);
            _cachedObject = null;
        }

        // public UniTask InstantiateAsync<T>(AssetReference assetReference, Transform parent) where T : Object
        // {
        //     throw new System.NotImplementedException();
        // }
    }
}