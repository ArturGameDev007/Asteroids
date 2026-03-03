using UnityEngine;

namespace _Project.Scripts.Configs.Player
{
    [CreateAssetMenu(fileName = "LaserConfig", menuName = "Configs/PlayerController/Weapon/LaserConfig", order = 51)]
    public class LaserConfig : ScriptableObject
    {
        [field: SerializeField] public int MaxAmountLaser { get; private set; } = 20;
        [field: SerializeField] public float ReloadTime { get; private set; } = 5f;
    }
}