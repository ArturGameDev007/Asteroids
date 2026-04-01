using System;
using Zenject;

namespace _Project.Scripts.UI.GameScreen
{
    public class LosePresenter : IInitializable, IDisposable
    {
        private readonly ILoseModel _loseModel;

        private ILoseView _loseView;

        public event Action OnRestartClick;

        public LosePresenter(ILoseView loseView, ILoseModel loseModel)
        {
            _loseView = loseView;
            _loseModel = loseModel;
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

            if (_loseView != null)
                _loseView.OnRestartRequested -= OnRestartRequested;
        }

        public void Open(int finalScore)
        {
            _loseModel.SaveResult(finalScore);
            _loseView.ShowPanel();

            UpdateScoreView();
        }

        public void Close()
        {
            _loseView?.HidePanel();
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