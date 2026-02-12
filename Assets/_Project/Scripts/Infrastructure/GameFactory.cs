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

        public void CreateBackground(BackgroundView prefab, Camera  mainCamera)
        {
            int orderInLayer = -5;

            BackgroundView background = _instantiator.CreatePrefab(prefab);
            background.name = "UI - Background";
    
            SetHierarchy(background.transform, 3);

            background.Construct(mainCamera, orderInLayer);
            
            
            // int orderInLayer = -5;
            //
            // BackgroundView background = _instantiator.CreatePrefab(prefab);
            // background.name = "UI - Background";
            //
            // SetHierarchy(background.transform, 3);
            //
            // if (background.TryGetComponent(out Canvas canvas))
            // {
            //     canvas.worldCamera = mainCamera; 
            //     canvas.sortingOrder = orderInLayer;
            // }
        }

        public void CreatePlayer(Character prefab, out Character character, out PlayerController controller,
            out InputForShoot shoot)
        {
            if (prefab == null) {
                Debug.LogError("Префаб игрока не передан в фабрику!");
                character = null; controller = null; shoot = null;
                return;
            }
            
            Character playerObject = _instantiator.CreatePrefab(prefab);
            playerObject.name = "Ship_Player";

            SetHierarchy(playerObject.transform, 2);

            playerObject.TryGetComponent(out character);
            playerObject.TryGetComponent(out controller);
            playerObject.TryGetComponent(out shoot);

            if (character == null || controller == null || shoot == null)
                Debug.LogError("На префабе игрока не хватает компонентов!");
        }

        public void CreatePerformanceShip(PerformanceShipView prefab, Character player, PlayerController controller,
            InputForShoot shoot, HierarchyScanner scanner)
        {
            PerformanceShipView performanceShip = _instantiator.CreatePrefab(prefab);
            performanceShip.name = "UI - Performance Ship";

            SetHierarchy(performanceShip.transform, 4);

            if (performanceShip.TryGetComponent(out CoordinateDisplay display))
            {
                Rigidbody2D head = player?.GetComponent<Rigidbody2D>();

                display?.Initialize(controller, head);
            }

            if (scanner.TryGetInStack(performanceShip.transform, out ViewCurrentAmountLaser viewLaser))
                viewLaser?.Initialize();

            WeaponShooter shooter = new WeaponShooter();

            if (scanner.TryGetInStack(performanceShip.transform, out GenerateLaser laserLogic))
                shoot?.Initialize(laserLogic, shooter);
        }

        public void CreateEndGameScreen(EndGameView prefab, HierarchyScanner scanner, out LoseViewModel viewModel,
            out ViewScore score)
        {
            viewModel = null;
            score = null;

            EndGameView gameScreen = _instantiator.CreatePrefab(prefab);
            gameScreen.name = "UI - EndGameScreen";

            SetHierarchy(gameScreen.transform, 5);

            if (scanner.TryGetInStack(gameScreen.transform, out LoseViewModel loseViewModel))
            {
                viewModel = loseViewModel;

                if (scanner.TryGetInStack(gameScreen.transform, out LoseView loseView))
                {
                    if (scanner.TryGetInStack(loseView.transform, out Button button))
                    {
                        loseView.Construct(button);
                    }

                    viewModel.Construct(loseView);
                }
            }

            if (scanner.TryGetInStack(gameScreen.transform, out ViewScore viewScore))
                score = viewScore;
        }

        private void SetHierarchy(Transform target, int index)
        {
            target.SetSiblingIndex(index);
        }
    }
}