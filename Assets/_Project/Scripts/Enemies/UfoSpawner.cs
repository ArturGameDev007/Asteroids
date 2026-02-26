using UnityEngine;

namespace _Project.Scripts.Enemies
{
    public class UfoSpawner : GeneratorEnemies
    {
        private Transform _player;

        public void Construct(Transform player)
        {
            _player = player;
        }

        protected override void ConfigureSpawn(Enemy enemy, Vector2 _)
        {
            if (enemy.TryGetComponent(out FlyingSaucerController flyingSaucerController))
            {
                flyingSaucerController.Construct(_player);
                flyingSaucerController.enabled = true;
            }
        }
    }
}