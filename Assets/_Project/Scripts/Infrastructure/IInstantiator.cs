using UnityEngine;

namespace _Project.Scripts.Infrastructure
{
    public interface IInstantiator
    {
        public T CreatePrefab<T>(T prefab) where T : Component;
    }
}