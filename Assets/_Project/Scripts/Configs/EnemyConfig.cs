using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.Configs
{
    [CreateAssetMenu(fileName = "NewEnemyConfig", menuName = "Configs/EnemyConfig", order = 51)]
    public class EnemyConfig : ScriptableObject
    {
        [field: SerializeField] public float SpawnOffset { get; private set; } = 2.0f;
        [field: SerializeField] public float Delay { get; private set; } = 3f;
        [field: SerializeField] public int ScoreForKill { get; private set; }
    }
}