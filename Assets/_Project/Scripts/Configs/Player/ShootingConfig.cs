using UnityEngine;

namespace _Project.Scripts.Configs.Player
{
    public class ShootingConfig : ScriptableObject
    {
        [field: Header("Projectile Settings")]
        [field: SerializeField] public float Speed { get; private set; } = 5f;
        [field: SerializeField] public float LifeTime { get; private set; } = 2.5f;
    }
}