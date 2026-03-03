using UnityEngine;

namespace _Project.Scripts.Configs.PoolObjects
{
    [CreateAssetMenu(fileName = "PoolConfig", menuName = "Configs/PoolConfig", order = 51)]
    public class PoolConfig : ScriptableObject
    {
        [field: Header("Enemies Pool Config")]
        [field: SerializeField] public int AsteroidPoolSize { get; private set; } = 5;
        [field: SerializeField] public int UfoPoolSize { get; private set; } = 5;
        
        [field: Header("Weapons Pool Config")]
        [field: SerializeField] public int BulletPoolSize { get; private set; } = 5;
        [field: SerializeField] public int LaserPoolSize { get; private set; } = 10;
    }
}