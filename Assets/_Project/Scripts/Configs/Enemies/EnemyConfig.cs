using UnityEngine;

namespace _Project.Scripts.Configs.Enemies
{
    public abstract class EnemyConfig : ScriptableObject
    {
        [field: Header("General Settings")]
        [field: SerializeField] public float Speed { get; private set; } = 1.5f;
        [field: SerializeField] public float SpawnOffset { get; private set; } = 2.0f;
        [field: SerializeField] public float Delay { get; private set; } = 3f;
        [field: SerializeField] public int ScoreForKill { get; private set; }
    }
}