using _Project.Scripts.Enemies;
using _Project.Scripts.Player;
using _Project.Scripts.Player.Weapons;

namespace _Project.Scripts.Infrastructure
{
    public class GameplayController
    {
        private readonly EnemySpawnController _enemySpawnController;
        private readonly IControllable _controller;
        private readonly IShootable _shoot;
        private readonly Character _player;

        private bool _isActive =  true;

        public GameplayController(EnemySpawnController enemySpawnController, IControllable controller, IShootable shoot, Character player)
        {
            _enemySpawnController = enemySpawnController;
            _controller = controller;
            _shoot = shoot;
            _player = player;
        }

        public void Update(float deltaTime)
        {
            if (!_isActive)
                return;
            
            _enemySpawnController.Process(deltaTime);
        }
        
        public void ContinueGame()
        {
            _isActive = true;
            _player.Revive();
            _controller?.ResetState();
            _controller?.EnableControl();
            _shoot.EnableControl();
            _enemySpawnController.StartAll();
        }

        public void StopGameplay()
        {
            _isActive = false;
            _controller?.StopPhysics();
            _controller?.DisableControl();
            _shoot.DisableControl();
            _shoot.StopAllShoots();
            _enemySpawnController.StopAndClearAll();
        }
    }
}