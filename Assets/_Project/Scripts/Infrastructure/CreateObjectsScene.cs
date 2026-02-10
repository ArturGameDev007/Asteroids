using _Project.Scripts.Player;
using UnityEngine;

namespace _Project.Scripts.Infrastructure
{
    public class CreateObjectsScene : MonoBehaviour, IInstantiator
    {
        public T CreatePrefab<T>(T prefab) where T : Component
        {
            if (prefab == null) return null;
            T instance = Instantiate(prefab, prefab.transform.position, prefab.transform.rotation);
            
            return instance;
        }
    }
}