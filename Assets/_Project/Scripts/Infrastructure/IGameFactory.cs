using _Project.Scripts.Player;
using _Project.Scripts.Player.Weapons;
using _Project.Scripts.UI.GameScreen;
using _Project.Scripts.UI.PerformanceShip;
using _Project.Scripts.Utils;
using UnityEngine;

namespace _Project.Scripts.Infrastructure
{
    public interface IGameFactory
    {
        public void CreateBackground(GameObject prefab, Camera mainCamera);
        public void CreatePlayer(GameObject prefab, out Character character, out PlayerController controller, out InputForShoot shoot);
        public void CreatePerformanceShip(GameObject prefab, Character player, PlayerController controller, InputForShoot shoot, CoordinateDisplay coordinateDisplay, ViewCurrentAmountLaser amountLaser, HierarchyScanner scanner);
        public void CreateEndGameScreen(GameObject prefab, HierarchyScanner scanner, out LoseViewModel viewModel, out ViewScore viewScore);
    }
}