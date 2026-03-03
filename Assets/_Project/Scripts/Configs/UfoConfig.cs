using UnityEngine;

namespace _Project.Scripts.Configs
{
    [CreateAssetMenu(fileName = "UfoConfig", menuName = "Configs/Enemies/Ufo", order = 51)]
    public class UfoConfig : EnemyConfig
    {
        [field: SerializeField] public float RotationSpeed { get; private set; } = 30f;
    }
}