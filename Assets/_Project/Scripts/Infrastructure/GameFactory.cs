using _Project.Scripts.UI.GameScreen;
using Zenject;

namespace _Project.Scripts.Infrastructure
{
    public class GameFactory : IGameFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly ILoseModel _loseModel;

        public GameFactory(IInstantiator instantiator, ILoseModel loseModel)
        {
            _instantiator = instantiator;
            _loseModel = loseModel;
        }

        public LosePresenter CreateLoseScreen(LoseView prefab)
        {
            LoseView loseView = _instantiator.InstantiatePrefabForComponent<LoseView>(prefab);

            var presenter = new LosePresenter(loseView, _loseModel);
            presenter.Initialize();

            return presenter;
        }
    }
}