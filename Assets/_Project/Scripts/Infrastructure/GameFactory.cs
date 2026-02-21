using _Project.Scripts.Player;
using _Project.Scripts.Player.Weapons;
using _Project.Scripts.UI.Background;
using _Project.Scripts.UI.GameScreen;
using _Project.Scripts.UI.PerformanceShip;
using _Project.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Infrastructure
{
    public class GameFactory : IGameFactory
    {
        private readonly IInstantiator _instantiator;

        public GameFactory(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public void CreateBackground(BackgroundView prefab, Camera mainCamera)
        {
            int orderInLayer = -5;

            BackgroundView background = _instantiator.CreatePrefab(prefab);

            SetHierarchy(background.transform, 3);

            background.Construct(mainCamera, orderInLayer);
        }

        public void CreatePlayer(Character prefab, out Character character, out PlayerController controller,
            out InputForShoot shoot)
        {
            if (prefab == null)
                throw new MissingReferenceException("Префаб игрока не передан!");

            Character playerObject = _instantiator.CreatePrefab(prefab);

            SetHierarchy(playerObject.transform, 2);

            playerObject.TryGetComponent(out character);
            playerObject.TryGetComponent(out controller);
            playerObject.TryGetComponent(out shoot);

            if (character == null || controller == null || shoot == null)
                Debug.LogError("На префабе игрока не хватает компонентов!");
        }

        public void CreatePerformanceShip(PerformanceShipView prefab, Character player, PlayerController controller,
            InputForShoot shoot, WeaponShooter shooter, HierarchyScanner scanner)
        {
            PerformanceShipView performanceShip = _instantiator.CreatePrefab(prefab);

            SetHierarchy(performanceShip.transform, 4);

            if (performanceShip.TryGetComponent(out CoordinateDisplay display))
            {
                Rigidbody2D head = player?.GetComponent<Rigidbody2D>();

                display?.Initialize(controller, head);
            }

            if (scanner.TryGetInStack(performanceShip.transform, out ViewCurrentAmountLaser viewLaser))
                viewLaser?.Initialize();

            if (scanner.TryGetInStack(performanceShip.transform, out GenerateLaser laserLogic))
                shoot?.Initialize(laserLogic, shooter);
        }

        public void CreateEndGameScreen(EndGameView prefab, HierarchyScanner scanner, ScoreData scoreData,
            out LosePresenter presenter)
        {
            EndGameView endGameContainer = _instantiator.CreatePrefab(prefab);

            SetHierarchy(endGameContainer.transform, 5);

            if (scanner.TryGetInStack(endGameContainer.transform, out LoseView loseView))
            {
                if (scanner.TryGetInStack(loseView.transform, out Button button))
                    loseView.Construct(button);

                presenter = new LosePresenter();
                presenter.Construct(scoreData, loseView);
                presenter.Enable();

                endGameContainer.Construct(loseView);
            }
            else
            {
                presenter = null;
            }
        }

        private void SetHierarchy(Transform target, int index)
        {
            target.SetSiblingIndex(index);
        }
    }
}