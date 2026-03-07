using System;
using Zenject;

namespace _Project.Scripts.UI.GameScreen
{
    public class LosePresenter
    {
        private ILoseModel _loseModel;
        private ILoseView _loseView;
        
        public event Action OnRestartClick;

        [Inject]
        public LosePresenter(ILoseModel loseModel, ILoseView loseView)
        {
            _loseModel = loseModel;
            _loseView = loseView;
        }

        public void Open(int finalScore)
        {
            _loseModel.SaveResult(finalScore);
            _loseView.ShowPanel();
            Enable();
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
            
            if (_loseView is IDisposable disposableView) 
                disposableView.Dispose();
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