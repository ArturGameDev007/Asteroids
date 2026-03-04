using UnityEngine;

namespace _Project.Scripts.Configs.Player
{
    [CreateAssetMenu(fileName = "Laser", menuName = "Configs/PoolConfigs/ShootingConfigs/Laser", order = 51)]
    public class LaserConfig : ShootingConfig
    {
        [field: Header("Ability Settings")]
        [field: SerializeField] public int MaxAmountLaser { get; private set; } = 20;
        [field: SerializeField] public float ReloadTime { get; private set; } = 5f;
    }
}