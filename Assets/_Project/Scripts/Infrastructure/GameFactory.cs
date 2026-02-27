using _Project.Scripts.Player;
using _Project.Scripts.Player.Weapons;
using _Project.Scripts.UI.Background;
using _Project.Scripts.UI.GameScreen;
using _Project.Scripts.UI.PerformanceShip;
using UnityEngine;

namespace _Project.Scripts.Infrastructure
{
    public class GameFactory : IGameFactory
    {
        public void CreateBackground(Canvas prefab, Camera mainCamera)
        {
            int orderInLayer = -5;

            Canvas background = Object.Instantiate(prefab);
            SetHierarchy(background.transform, 3);

            BackgroundView backgroundView = new BackgroundView(background);
            backgroundView.Construct(mainCamera, orderInLayer);
        }

        public void CreatePlayer(Character prefab, out Character character, out PlayerController controller,
            out InputForShoot shoot)
        {
            if (prefab == null)
                throw new MissingReferenceException("Префаб игрока не передан!");

            Character playerObject = Object.Instantiate(prefab);
            SetHierarchy(playerObject.transform, 2);

            playerObject.TryGetComponent(out character);
            playerObject.TryGetComponent(out controller);
            playerObject.TryGetComponent(out shoot);

            if (character == null || controller == null || shoot == null)
                Debug.LogError("На префабе игрока не хватает компонентов!");
        }

        public void CreatePerformanceShip(PerformanceShipView prefab, Character player, PlayerController controller,
            InputForShoot shoot, WeaponShooter shooter)
        {
            PerformanceShipView performanceShip = Object.Instantiate(prefab);
            SetHierarchy(performanceShip.transform, 4);

            if (performanceShip.TryGetComponent(out CoordinateDisplay display))
            {
                Rigidbody2D head = player?.GetComponent<Rigidbody2D>();
                display?.Initialize(controller, head);
            }
            
            if (performanceShip.GenerateLaser != null && performanceShip.ViewCurrentAmountLaser != null)
            {
                shoot?.Initialize(performanceShip.GenerateLaser, shooter);
                
                performanceShip.ViewCurrentAmountLaser.Initialize();
        
                performanceShip.Construct(performanceShip.CoordinateDisplay, performanceShip.ViewCurrentAmountLaser, performanceShip.GenerateLaser);
            }
        }

        public void CreateEndGameScreen(EndGameView prefab, ScoreData scoreData,
            out LosePresenter presenter)
        {
            EndGameView endGameContainer = Object.Instantiate(prefab);
            SetHierarchy(endGameContainer.transform, 5);
            
            if (endGameContainer.TryGetComponent(out LoseView loseView))
            {
                loseView.Construct(loseView.RestartButton);

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