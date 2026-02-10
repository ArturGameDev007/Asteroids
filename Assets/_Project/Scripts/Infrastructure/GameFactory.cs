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
    public class GameFactory : MonoBehaviour, IGameFactory
    {
        // private readonly IInstantiator _instantiator;
        //
        // public GameFactory(IInstantiator instantiator)
        // {
        //     _instantiator = instantiator;
        // }

        public void CreateBackground(GameObject prefab, Camera  mainCamera)
        {
            int orderInLayer = -5;

            GameObject background = Instantiate(prefab);
            background.name = "UI - Background";
            

            if (background.TryGetComponent(out Canvas canvas))
            {
                // canvas.renderMode = RenderMode.ScreenSpaceCamera;
                canvas.worldCamera = mainCamera; 
                canvas.sortingOrder = orderInLayer;
            }
            SetHierarchy(background.transform, 3);
        }

        public void CreatePlayer(GameObject prefab, out Character character, out PlayerController controller,
            out InputForShoot shoot)
        {
            GameObject playerObject = Instantiate(prefab, Vector2.zero, Quaternion.identity);
            playerObject.name = "Ship_Player";

            SetHierarchy(playerObject.transform, 2);

            playerObject.TryGetComponent(out character);
            playerObject.TryGetComponent(out controller);
            playerObject.TryGetComponent(out shoot);

            if (character == null || controller == null || shoot == null)
                Debug.LogError("На префабе игрока не хватает компонентов!");
        }

        public void CreatePerformanceShip(GameObject prefab, Character player, PlayerController controller,
            InputForShoot shoot, HierarchyScanner scanner)
        {
            // PerformanceShipView performanceShip = _instantiator.CreatePrefab(prefab);
            GameObject performanceShip = Instantiate(prefab);
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

        public void CreateEndGameScreen(GameObject prefab, HierarchyScanner scanner, out LoseViewModel viewModel,
            out ViewScore score)
        {
            viewModel = null;
            score = null;

            // EndGameView gameScreen = _instantiator.CreatePrefab(prefab);
            GameObject gameScreen = Instantiate(prefab);
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