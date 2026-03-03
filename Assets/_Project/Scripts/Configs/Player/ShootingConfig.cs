using UnityEngine;

namespace _Project.Scripts.Configs.Player
{
    [CreateAssetMenu(fileName = "", menuName = "Configs/PlayerController/Weapon/Shooting", order = 51)]
    public class ShootingConfig : ScriptableObject
    {
        [field: SerializeField] public float Speed { get; private set; } = 5f;
        [field: SerializeField] public float LifeTime { get; private set; } = 2.5f;
    }
}