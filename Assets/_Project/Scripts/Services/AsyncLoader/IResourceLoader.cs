using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Services.AsyncLoader
{
    public interface IResourceLoader
    {
        public UniTask<T> LoadResourceAsync<T>(string adress) where T: Object;
        public void Unload<T>(T asset) where T: Object;
    }
}