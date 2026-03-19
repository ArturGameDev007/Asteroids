using _Project.Scripts.Services.Analytics;
using _Project.Scripts.UI.GameScreen;
using Zenject;

namespace _Project.Scripts.Infrastructure
{
    public class GameFactory : IGameFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly IAnalyticsService _analyticsService;

        public GameFactory(IInstantiator instantiator, IAnalyticsService analyticsService)
        {
            _instantiator = instantiator;
            _analyticsService = analyticsService;
        }

        public LosePresenter CreateEndGameScreen(LoseView prefab, ILoseModel scoreData)
        {
            LoseView loseView = _instantiator.InstantiatePrefabForComponent<LoseView>(prefab);

            if (loseView != null)
            {
                var presenter = new LosePresenter(scoreData, loseView, _analyticsService);
                presenter.Initialize();
                return presenter;
            }

            return null;
        }
    }
}