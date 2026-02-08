using UnityEngine;

namespace _Project.Scripts.Infrastructure
{
    public interface IInstantiator
    {
        public GameObject CreatePrefab(GameObject prefab);
    }
}