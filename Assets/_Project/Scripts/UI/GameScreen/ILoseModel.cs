using System;

namespace _Project.Scripts.UI.GameScreen
{
    public interface ILoseModel
    {
        public event Action<int> OnScoreChanged;
        public int FinalScore { get; }
        public void SaveResult(int score);
    }
}