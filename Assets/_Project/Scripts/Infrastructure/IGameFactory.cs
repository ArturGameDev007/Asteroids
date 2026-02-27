using _Project.Scripts.Player;
using _Project.Scripts.Player.Weapons;
using _Project.Scripts.UI.GameScreen;
using _Project.Scripts.UI.PerformanceShip;
using UnityEngine;

namespace _Project.Scripts.Infrastructure
{
    public interface IGameFactory
    {
        public void CreateBackground(Canvas prefab, Camera mainCamera);
        public void CreatePlayer(Character prefab, out Character character, out PlayerController controller, out InputForShoot shoot);
        public void CreatePerformanceShip(PerformanceShipView prefab, Character player, PlayerController controller, InputForShoot shoot, WeaponShooter shooter);
        public void CreateEndGameScreen(EndGameView prefab, ScoreData scoreData, out LosePresenter losePresenter);
    }
}