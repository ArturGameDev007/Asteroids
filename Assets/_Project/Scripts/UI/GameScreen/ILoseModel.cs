using System;

namespace _Project.Scripts.UI.GameScreen
{
    public interface ILoseModel
    {
        public event Action OnScoreChanged;
        public int Score { get; }
        public void SaveResult(int score);
    }
}