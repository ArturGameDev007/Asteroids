using System;
using _Project.Scripts.Services.RemoteConfigs;

namespace _Project.Scripts.UI.GameScreen
{
    public interface ILoseModel
    {
        public event Action OnScoreChanged;
        
        public int Score { get; }
        public int BestScore { get; }
        
        public void Reset();

        public void AddScore(IRemoteConfigs  config);
        
        public void SaveResult(int score);
    }
}