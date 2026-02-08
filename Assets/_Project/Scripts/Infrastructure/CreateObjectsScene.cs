using UnityEngine;

namespace _Project.Scripts.Infrastructure
{
    public class CreateObjectsScene : MonoBehaviour, IInstantiator
    {
        public GameObject CreatePrefab(GameObject prefab)
        {
            Instantiate(prefab, prefab.transform.position, prefab.transform.rotation);
            
            return prefab;
        }
    }
}