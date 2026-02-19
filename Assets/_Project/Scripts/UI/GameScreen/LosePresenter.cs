using System;

namespace _Project.Scripts.UI.GameScreen
{
    public class LosePresenter
    {
        private ILoseModel _loseModel;
        private ILoseView _loseView;
        
        public event Action OnRestartClick;

        public void Construct(ILoseModel model, ILoseView view)
        {
            _loseModel = model;
            _loseView = view;
        }

        public void Open(int finalScore)
        {
            _loseModel.SaveResult(finalScore);
            _loseView.ShowPanel();
        }

        public void Close()
        {
            _loseView.HidePanel();
        }
        
        public void Enable()
        {
            Subscribe();
            OnScoreChanged(_loseModel.FinalScore);
        }

        public void Dispose()
        {
            Unsubscribe();
        }
        
        private void Subscribe()
        {
            _loseModel.OnScoreChanged += OnScoreChanged;
            _loseView.OnRestartRequested += OnRestartRequested;
        }

        private void Unsubscribe()
        {
            _loseModel.OnScoreChanged -= OnScoreChanged;
            _loseView.OnRestartRequested -= OnRestartRequested;
        }

        private void OnScoreChanged(int score)
        {
            _loseView.SetScore(score);
        }

        private void OnRestartRequested()
        {
            OnRestartClick?.Invoke();
        }
    }
}