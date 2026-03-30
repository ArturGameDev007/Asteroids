using _Project.Scripts.UI.GameScreen;
using _Project.Scripts.UI.PerformanceShip;

namespace _Project.Scripts.Infrastructure
{
    public interface IGameFactory
    {
        // public PerformancePresenter CreatePerformanceShip(CoordinateDisplay view);
        
        public LosePresenter CreateLoseScreen(LoseView prefab);
    }
}