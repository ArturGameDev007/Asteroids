using System;
using Zenject;

namespace _Project.Scripts.UI.GameScreen
{
    public class LosePresenter: IInitializable, IDisposable
    {
        private readonly ILoseModel _loseModel;
        private readonly ILoseView _loseView;
        
        public event Action OnRestartClick;

        // [Inject]
        public LosePresenter(ILoseModel loseModel, ILoseView loseView)
        {
            _loseModel = loseModel;
            _loseView = loseView;
        }

        public void Open(int finalScore)
        {
            _loseModel.SaveResult(finalScore);
            _loseView.ShowPanel();
            
            Initialize();
        }

        public void Close()
        {
            _loseView.HidePanel();
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
            // Unsubscribe();
            
            if (_loseView is IDispose disposableView) 
                disposableView.Dispose();
        }
        
        // private void Enable()
        // {
        //     Subscribe();
        //     UpdateScoreView();
        // }
        
        // private void Subscribe()
        // {
        //     _loseModel.OnScoreChanged += UpdateScoreView;
        //     _loseView.OnRestartRequested += OnRestartRequested;
        // }
        //
        // private void Unsubscribe()
        // {
        //     _loseModel.OnScoreChanged -= UpdateScoreView;
        //     _loseView.OnRestartRequested -= OnRestartRequested;
        // }

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