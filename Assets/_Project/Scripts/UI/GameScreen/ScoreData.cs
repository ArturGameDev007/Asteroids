using System;
using _Project.Scripts.Services.RemoteConfigs;
using _Project.Scripts.Services.Save;

namespace _Project.Scripts.UI.GameScreen
{
    public class ScoreData: ILoseModel
    {
        public event Action OnScoreChanged;
        
        private readonly ISaveService _saveService;

        public int Score { get; private set; }
        public int BestScore { get; private set; }

        private int _zeroCountScore = 0;

        public ScoreData(ISaveService saveService)
        {
            _saveService = saveService;
        }

        public void Reset()
        {
            Score = _zeroCountScore;
            OnScoreChanged?.Invoke();
        }

        public void AddScore(IRemoteConfigs  config)
        {
            Score += config.RemoteConfig.EnemyConfigs.ScoreForKill;
            OnScoreChanged?.Invoke();
        }

        public void SaveResult(int score)
        {
            Score = score;

            var data = _saveService.Load();
            data.UpdateBestResult(score);
            _saveService.Save(data);

            BestScore = data.BestResult;
            
            OnScoreChanged?.Invoke();
        }
    }
}
