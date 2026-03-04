using UnityEngine;

namespace _Project.Scripts.Configs.Enemies
{
    [CreateAssetMenu(fileName = "UfoConfig", menuName = "Configs/PoolConfigs/Enemies/Ufo", order = 51)]
    public class UfoConfig : EnemyConfig
    {
        [field: Header("Ability Settings")]
        [field: SerializeField] public float RotationSpeed { get; private set; } = 30f;
    }
}