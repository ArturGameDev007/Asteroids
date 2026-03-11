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

        public LosePresenter CreateEndGameScreen(LoseView prefab, ILoseModel scoreData)
        {
            LoseView endGameView = _instantiator.InstantiatePrefabForComponent<LoseView>(prefab);
            
            if (endGameView.TryGetComponent(out LoseView loseView))
            {
                var presenter = new LosePresenter(scoreData, loseView);
                return presenter;
            }

            return null;
        }
    }
}