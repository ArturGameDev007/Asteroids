using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Project.Scripts.Services.AsyncLoader
{
    public class AddressableResourceLoader : IResourceLoader
    {
        public UniTask<T> LoadResourceAsync<T>(string adress) where T : Object
        {
            throw new System.NotImplementedException();
        }

        public void Unload<T>(T asset) where T : Object
        {
            Addressables.Release(asset);
        }
    }
}