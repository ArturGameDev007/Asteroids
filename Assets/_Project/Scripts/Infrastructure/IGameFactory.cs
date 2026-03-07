using _Project.Scripts.UI.GameScreen;

namespace _Project.Scripts.Infrastructure
{
    public interface IGameFactory
    {
        // public void CreateBackground(Canvas prefab, Camera mainCamera);
        // public void CreatePlayer(PlayerController prefab, out PlayerController controller, out InputForShoot shoot, out ICollisionHandler collisionHandler);
        // public void CreatePerformanceShip(PerformanceShipView prefab, PlayerController player, InputForShoot shoot, WeaponShooter shooter);
        public LosePresenter CreateEndGameScreen(EndGameView prefab, ILoseModel scoreData);
    }
}