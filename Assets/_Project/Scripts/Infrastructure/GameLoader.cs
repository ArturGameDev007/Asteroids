using _Project.Scripts.Enemies;
using _Project.Scripts.Player;
using _Project.Scripts.Player.Weapons;
using _Project.Scripts.UI.PerformanceShip;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Infrastructure
{
    public class GameLoader
    {
        private readonly PlayerResourceManager _playerResourceManager;
        private readonly CoordinateResourceManager _coordinateResourceManager;
        private readonly EnemyResourceManager _enemyResourceManager;
        private readonly ProjectileResourceManager _projectileResourceManager;

        public GameLoader(PlayerResourceManager playerResourceManager, CoordinateResourceManager coordinateResourceManager, EnemyResourceManager enemyResourceManager, ProjectileResourceManager projectileResourceManager)
        {
            _playerResourceManager = playerResourceManager;
            _coordinateResourceManager = coordinateResourceManager;
            _enemyResourceManager = enemyResourceManager;
            _projectileResourceManager = projectileResourceManager;
        }

        public async UniTask LoadAllAsync()
        {
            var loadPlayerAsync = _playerResourceManager.LoadAsync();
            var loadCoordinateAsync = _coordinateResourceManager.LoadAsyncPerformanceShip();
            var loadEnemiesAsync = _enemyResourceManager.LoadEnemiesAsync();
            var loadShotsAsync = _projectileResourceManager.LoadShotsAsync();

            await UniTask.WhenAll(loadPlayerAsync, loadCoordinateAsync, loadEnemiesAsync, loadShotsAsync);
        }

        public void UnloadAll()
        {
            _playerResourceManager.Unload();
            _coordinateResourceManager.Unload();
            _enemyResourceManager.UnloadEnemies();
            _projectileResourceManager.UnloadShots();
        }
    }
}