using _Project.Scripts.Enemies;
using UnityEngine;

namespace _Project.Scripts.Configs.Enemies
{
    [CreateAssetMenu(fileName = "AsteroidConfig", menuName = "Configs/PoolConfigs/Enemies/Asteroid", order = 51)]
    public class AsteroidConfig : EnemyConfig
    {
        public override GeneratorEnemies CreateSpawn()
        {
            return new AsteroidSpawner(this);
        }
    }
}