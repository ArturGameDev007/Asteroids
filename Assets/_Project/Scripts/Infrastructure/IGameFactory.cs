using _Project.Scripts.UI.GameScreen;

namespace _Project.Scripts.Infrastructure
{
    public interface IGameFactory
    {
        public LosePresenter CreateEndGameScreen(LoseView prefab, ILoseModel scoreData);
    }
}