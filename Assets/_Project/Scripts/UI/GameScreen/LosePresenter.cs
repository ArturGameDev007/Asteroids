using System;
using _Project.Scripts.Services.Analytics;
using Zenject;

namespace _Project.Scripts.UI.GameScreen
{
    public class LosePresenter: IInitializable, IDisposable
    {
        private readonly ILoseModel _loseModel;
        private readonly ILoseView _loseView;
        
        private readonly AnalyticsService _analyticsService;
        
        public event Action OnRestartClick;

        public LosePresenter(ILoseModel loseModel, ILoseView loseView,  AnalyticsService analyticsService)
        {
            _loseModel = loseModel;
            _loseView = loseView;
            _analyticsService = analyticsService;
        }
        
        public void Initialize()
        {
            _loseModel.OnScoreChanged += UpdateScoreView;
            _loseView.OnRestartRequested += OnRestartRequested;

            UpdateScoreView();
        }
        
        public void Dispose()
        {
            _loseModel.OnScoreChanged -= UpdateScoreView;
            _loseView.OnRestartRequested -= OnRestartRequested;
        }

        public void Open(int finalScore, int shots, int lasers, int destroyedEnemies)
        {
            _loseModel.SaveResult(finalScore);
            
            _loseView.ShowPanel();
            _loseView.SetStats(shots, lasers, destroyedEnemies);
            
            _analyticsService.SendGameEnd(shots, lasers, destroyedEnemies);

            UpdateScoreView();
        }

        public void Close()
        {
            _loseView.HidePanel();
        }

        private void UpdateScoreView()
        {
            _loseView.SetScore(_loseModel.Score);
        }

        private void OnRestartRequested()
        {
            OnRestartClick?.Invoke();
        }
    }
}