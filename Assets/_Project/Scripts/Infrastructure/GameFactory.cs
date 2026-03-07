using _Project.Scripts.UI.GameScreen;
using Zenject;

namespace _Project.Scripts.Infrastructure
{
    public class GameFactory : IGameFactory
    {
        private readonly IInstantiator _instantiator;

        public GameFactory(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        // public void CreateBackground(Canvas prefab, Camera mainCamera)
        // {
        //     Canvas background = Object.Instantiate(prefab);
        //     SetHierarchy(background.transform, 3);
        //
        //     BackgroundView backgroundView = new BackgroundView(background, mainCamera);
        // }

        // public void CreatePlayer(PlayerController prefab, out PlayerController controller, out InputForShoot shoot, out ICollisionHandler collisionHandler)
        // {
        //     if (prefab == null)
        //         throw new MissingReferenceException("Префаб игрока не передан!");
        //
        //     PlayerController playerObject = Object.Instantiate(prefab);
        //     SetHierarchy(playerObject.transform, 2);
        //
        //     playerObject.TryGetComponent(out controller);
        //     playerObject.TryGetComponent(out shoot);
        //     playerObject.TryGetComponent(out collisionHandler);
        //
        //     if (controller == null || shoot == null || collisionHandler == null)
        //         Debug.LogError("На префабе игрока не хватает компонентов!");
        // }

        // public void CreatePerformanceShip(PerformanceShipView prefab, PlayerController player,
        //     InputForShoot shoot, WeaponShooter shooter)
        // {
        //     PerformanceShipView performanceShip = Object.Instantiate(prefab);
        //     SetHierarchy(performanceShip.transform, 4);
        //
        //     if (performanceShip.TryGetComponent(out CoordinateDisplay display))
        //     {
        //         Rigidbody2D head = player?.GetComponent<Rigidbody2D>();
        //         display?.Initialize(player, head);
        //     }
        //     
        //     if (performanceShip.GenerateLaser != null && performanceShip.ViewCurrentAmountLaser != null)
        //     {
        //         shoot?.Construct(performanceShip.GenerateLaser, shooter);
        //         
        //         performanceShip.ViewCurrentAmountLaser.Construct();
        //         performanceShip.Construct(performanceShip.CoordinateDisplay, performanceShip.ViewCurrentAmountLaser, performanceShip.GenerateLaser);
        //     }
        // }

        public LosePresenter CreateEndGameScreen(EndGameView prefab, ILoseModel scoreData)
        {
            EndGameView endGameView = _instantiator.InstantiatePrefabForComponent<EndGameView>(prefab);
            
            if (endGameView.TryGetComponent(out LoseView loseView))
            {
                var presenter = new LosePresenter(scoreData, loseView);
                return presenter;
            }

            return null;
        }
    }
}