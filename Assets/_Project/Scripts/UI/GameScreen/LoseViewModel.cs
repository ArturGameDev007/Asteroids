using UnityEngine;

namespace _Project.Scripts.UI.GameScreen
{
    public class LoseViewModel
    {
        private readonly RestartGame _restartGame;
        private readonly LoseView  _loseView;
        private readonly GameTimeController _gameTimeController;
        
        
        public LoseViewModel(RestartGame restartGame, LoseView loseView, GameTimeController gameTimeController)
        {
            _restartGame = restartGame;
            _loseView = loseView;
            _gameTimeController = gameTimeController;
        }
        
        public void Initialize()
        {
            _player.OnDead += Show;
            BindButtons();
        }

        public void Dispose()
        {
            _player.OnDead -= Show;
            _loseCommand.Dispose();
        }


        private void BindButtons()
        {
            _loseCommand.Subscribe(_ => RestartGame());
            _loseView.action.OnClickAsObservable()
                .Subscribe(_loseCommand.Execute);
        }

        private void Show()
        {
            _loseView.Show();
            _gameTimeController.LoseGame();
        }

        private void RestartGame()
        {
            _restartGame.RestartScene();
        }
    }
}