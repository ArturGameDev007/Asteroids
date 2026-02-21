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
            UpdateScoreView();
        }

        public void Dispose()
        {
            Unsubscribe();
        }
        
        private void Subscribe()
        {
            _loseModel.OnScoreChanged += UpdateScoreView;
            _loseView.OnRestartRequested += OnRestartRequested;
        }

        private void Unsubscribe()
        {
            _loseModel.OnScoreChanged -= UpdateScoreView;
            _loseView.OnRestartRequested -= OnRestartRequested;
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