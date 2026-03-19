using System;

namespace _Project.Scripts.UI.GameScreen
{
    public interface ILoseView
    {
        public event Action OnRestartRequested;
        
        public void SetScore(int score);

        public void SetStats(int shots, int laser,  int destroyedEnemies);
        
        public void ShowPanel();
        
        public void HidePanel();
    }
}